using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMessages.MessageExtraction
{
    public static class ExtensionMethods
    {

        public static String BaseTable = "BodyParts";

        public static DataSet getDtaDataSet(this string SelectCmdString, string ConnectionString)
        {
            SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(SelectCmdString, ConnectionString);
            mySqlDataAdapter.SelectCommand.CommandTimeout = 6000;
            DataSet myDataSet = new DataSet();
            mySqlDataAdapter.Fill(myDataSet, BaseTable);
            return myDataSet;
        }
    }
}
