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
        List<DataRow>

        public string message;
        public string context

            public Message (List<DataRow> datavalues)
	{
                  
                datavalues.ForEach(row => {
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


                            SqlBinary binData = new SqlBinary((byte[]) row["imgContext"]);
                            MemoryStream stm = new MemoryStream(binData.Value);
                            String answer = "";
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
                                answer += propName + "; " + propValue + "\n";

                            }
                        }


                    }

    }
}
