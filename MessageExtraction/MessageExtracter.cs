using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.BizTalk.Agent.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using SaveMessages.HelperClass;
using System.Runtime.InteropServices;

namespace SaveMessages.MessageExtraction
{
    public class MessageExtracter
    {
        public String ConnectionString { get; set; }

        //zet een extra directory waar naar non dotnet assemblies wordt gezocht
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetDllDirectory(string lpPathName);


        
        public static Stream getMsgStrm(Stream stream)
        {
            SetDllDirectory(Directory.GetCurrentDirectory() + @"\binary");
            Assembly pipelineAssembly = Assembly.LoadFrom(@"Microsoft.BizTalk.Pipeline.dll");
            Type compressionStreamsType = pipelineAssembly.GetType("Microsoft.BizTalk.Message.Interop.CompressionStreams", true);
            return (Stream)compressionStreamsType.InvokeMember("Decompress", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static, null, null, new object[] { (object)stream });
        }


        public NameValueCollection GetHostsSqlforCurrentConnection()
        {
            NameValueCollection Collection = new NameValueCollection();
            try
            {

                Collection = new NameValueCollection();;
                DataSet set = File.ReadAllText("getHostSQL.sql").getDtaDataSet(ConnectionString);
                foreach (DataRow row in set.Tables[ExtensionMethods.BaseTable].Rows)
                {
                    Collection.Add(row["host"].ToString(), row["sql"].ToString() );
                }
            }
            catch (Exception ex)
            {

                // do logging
            }


            return Collection;


        }



        public void SaveApplicationToFiles(string applicationName, string appDirectory, bool DirectoryForInstanceType, bool DirectoryForInstance)
        {
            var x = new DataCollector(){ConnectionString = this.ConnectionString}.CollectHostInstancesFromApplication(applicationName);
            savetofiles(x,appDirectory,DirectoryForInstanceType,DirectoryForInstance);            

        }

        public void savetofiles(List<Instance> instances ,  string appDirectory, bool DirectoryForInstanceType, bool DirectoryForInstance)
        {




            try
            {
            
                bw.setText("Storing Messages");
                int progress = 1;
                foreach (DataRow row in myDataSet.Tables[ExtensionMethods.BaseTable].Rows)
                {
                    string directory = (DirectoryForInstanceType) 
                        ? appDirectory + @"\" + row["applicatie"] + @"\" + row["InstanceType"]
                        : appDirectory + @"\" + row["applicatie"];


                    directory = (DirectoryForInstance) ? directory + @"\" + row["instanceid"] : directory;
                    
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    if (row["imgPart"].GetType() != typeof (DBNull))
                    {

                        // eerste gedeelte van de file staat altijd in imgpart de rest in imgfrag
                        if (!File.Exists(directory + @"\" + row["uidPartID"].ToString() + ".xml"))
                        {

                            SqlBinary binData = new SqlBinary((byte[]) row["imgPart"]);
                            MemoryStream stm = new MemoryStream(binData.Value);
                            Stream aStream = getMsgStrm(stm);
                            StreamReader aReader = new StreamReader(aStream);
                            String aMessage = aReader.ReadToEnd();
                            File.AppendAllText(directory + @"\" + row["uidPartID"] + ".xml",
                                aMessage, Encoding.UTF8);
                        }

                        if (row["imgFrag"].GetType() != typeof (DBNull))
                        {

                            SqlBinary binData = new SqlBinary((byte[]) row["imgFrag"]);
                            MemoryStream stm = new MemoryStream(binData.Value);
                            Stream aStream = getMsgStrm(stm);
                            StreamReader aReader = new StreamReader(aStream);
                            String aMessage = aReader.ReadToEnd();
                            File.AppendAllText(directory + @"\" + row["uidPartID"] + ".xml",
                                aMessage, Encoding.UTF8);


                        }
                    }

                    if (row["imgContext"].GetType() != typeof(DBNull) && (! File.Exists(
                                directory  + @"\" + row["uidPartID"].ToString() + ".cont.xml")))
                    {
                        try
                        {


                            SqlBinary binData = new SqlBinary((byte[])row["imgContext"]);
                            MemoryStream stm = new MemoryStream(binData.Value);
                            String answer = "";
                            IBaseMessageContext context =
                                ((IBTMessageAgentFactory)((IBTMessageAgent)new BTMessageAgent())).CreateMessageContext
                                    ();
                            ((IPersistStream)context).Load(stm);
                            for (int i = 0; i < context.CountProperties; ++i)
                            {
                                string propName;
                                string propNamespace;
                                object propValue = context.ReadAt(i, out propName, out propNamespace);
                                Console.Out.WriteLine(propNamespace + ", " + propName + ": " +
                                                             propValue);
                                answer += propName + "; " + propValue + "\n";

                            }

                            if (InstanceIDinContext) answer += "instanceid ; " + row["instanceid"] + Environment.NewLine;
                            answer += "dtCreated ; " + row["dtCreated"] + Environment.NewLine;
                            File.AppendAllText(
                                directory  + @"\" + row["uidPartID"] + ".cont.xml", answer,
                                Encoding.UTF8);
                        }
                        catch (Exception exep)
                        {

                            ShowAllExceptions(exep);
                        }
                    }

                    if (bw != null)
                    {
                        bw.ReportProgress((int)((double)progress / (double)myDataSet.Tables[ExtensionMethods.BaseTable].Rows.Count * 100));
                        progress++;
                    }


                }
            }
            catch (Exception ex)
            {

                ShowAllExceptions(ex);
            }
        }



        public void ShowAllExceptions(Exception ex)
        {
            if (ex.InnerException != null) ShowAllExceptions(ex.InnerException);
            MessageBox.Show(ex.Message + ">>" + ex.StackTrace);


        }


        public TaskedBackgroundWorker bw;

    }
}
