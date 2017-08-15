using Microsoft.BizTalk.Message.Interop;
using SaveMessages.Pipeline.Overrides;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMessages.ExtensionMethods
{
    public static class Methods
    {

        public static String BaseTable = "BodyParts";

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);

        }


        public static DataSet getDtaDataSet(this string SelectCmdString, string ConnectionString)
        {
            SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(SelectCmdString, ConnectionString);
            mySqlDataAdapter.SelectCommand.CommandTimeout = 6000;
            DataSet myDataSet = new DataSet();
            mySqlDataAdapter.Fill(myDataSet, BaseTable);
            return myDataSet;
        }


        public static List<DataRow> toRowList(this DataTable table)
        {
            List<DataRow> lijst = new List<DataRow>();
            foreach (DataRow dataRow in table.Rows)
            {
                lijst.Add(dataRow);
            }
            return lijst;
        }

        //methode om onder andere private properties op te halen op basis van reflectie

        public static object getProperty(this object theobject, string propertyname)
        {
            try
            {
                Type type = theobject.GetType();
                return type.GetProperties().First(c => c.Name == propertyname).GetValue(theobject, null);
            }
            catch
            {
                return null;
            }
        }

        //splits de contextfile in keyvalue pairs en voegt deze toe aan de messagecontext
        public static void addContextFromFile(this IBaseMessage message, string file, string outpath)
        {
            message.Context = new ExtendedMessageContext();
            message.Context.Write("OutboundTransportLocation", PipelineConstants.SaveMessageNamespace, outpath + @"\%SourceFileName%");
            string[] items = File.ReadAllLines(file);
            foreach (var Item in items)
            {
                string[] a = Item.Split(';');
                if (a.Count() > 0) message.Context.Write(a[0], PipelineConstants.SaveMessageNamespace, a[1].Trim());
            }

        }


        public static List<DataRow> ToList(this DataRowCollection coll)
        {
            List<DataRow> answer = new List<DataRow>();
            foreach (DataRow rij in coll)
            {
                answer.Add(rij);
            }
            return answer;
        }


        public static string GetValidDirectoryString(this string thestring)
        {
            string fileName = thestring;
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }
            return fileName;
        }


    }
}
