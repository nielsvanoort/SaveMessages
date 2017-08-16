using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.BizTalk.Agent.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMessages.MessageExtraction
{
    public class Message
    {


        //zet een extra directory waar naar non dotnet assemblies wordt gezocht
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetDllDirectory(string lpPathName);

        public static Stream getMsgStrm(Stream stream)
        {
            SetDllDirectory(Directory.GetCurrentDirectory() + @"\binary");
            Assembly pipelineAssembly = Assembly.LoadFrom(@"Microsoft.BizTalk.Pipeline.dll");
            Type compressionStreamsType =
                pipelineAssembly.GetType("Microsoft.BizTalk.Message.Interop.CompressionStreams", true);
            return
                (Stream)
                    compressionStreamsType.InvokeMember("Decompress",
                        BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static, null, null,
                        new object[] {(object) stream});
        }



        public string Contents;
        public string Context;
        public string Messageid;
        public string Instanceid;
        public string MessageType;

        public NameValueCollection ContextValues
        {
            get
            {
                NameValueCollection collection = new NameValueCollection();
                Context.Split('\n').ToList<string>().ForEach(c =>
                {
                    if (!String.IsNullOrWhiteSpace(c))
                    {
                        string[] namevalue = c.Split(';');

                        collection.Add(namevalue[0], namevalue[1]);
                    }
                }
                    );

                return collection;
            }

        }


        public Message(List<DataRow> datavalues, string messageid, string instanceid)
        {
            this.Instanceid = instanceid;
            this.Messageid = messageid;
            datavalues.ForEach(row =>
            {
                if (MessageType == null && row["nvcMessageType"].GetType() != typeof (DBNull))
                {
                    MessageType = row["nvcMessageType"].ToString();
                }
                if (row["imgPart"].GetType() != typeof (DBNull))
                {

                    // eerste gedeelte van de file staat altijd in imgpart de rest in imgfrag
                    if (Contents == null) Contents = TheContents(row, "imgPart");
                    if (row["imgFrag"].GetType() != typeof (DBNull)) Contents += TheContents(row, "imgFrag");
                }

                if (row["imgContext"].GetType() != typeof (DBNull) && Context == null)
                {
                    try
                    {


                        SqlBinary binData = new SqlBinary((byte[]) row["imgContext"]);
                        MemoryStream stm = new MemoryStream(binData.Value);
                        Context = "";
                        IBaseMessageContext context =
                            ((IBTMessageAgentFactory) ((IBTMessageAgent) new BTMessageAgent())).CreateMessageContext
                                ();
                        ((IPersistStream) context).Load(stm);
                        for (int i = 0; i < context.CountProperties; ++i)
                        {
                            string propName;
                            string propNamespace;
                            object propValue = context.ReadAt(i, out propName, out propNamespace);
                            Console.Out.WriteLine(propNamespace + ", " + propName + ": " +
                                                  propValue);
                            Context += propName + "; " + propValue + "\n";

                        }

                        Context += "dtTimeStamp ; " + row["dtTimeStamp"] + "\n";
                        Context += "instanceid ; " + Instanceid + "\n";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }

                }
            });
        }

        private static string TheContents(DataRow row, string Cell)
        {
            SqlBinary binData = new SqlBinary((byte[]) row[Cell]);
            MemoryStream stm = new MemoryStream(binData.Value);
            Stream aStream = getMsgStrm(stm);
            StreamReader aReader = new StreamReader(aStream);
            string theContents = aReader.ReadToEnd();
            return theContents;
        }
    }
}
