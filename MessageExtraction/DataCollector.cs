using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.XLANGs.BaseTypes.DataSetStandIn;
using System.Data;

namespace SaveMessages.MessageExtraction
{
    public class DataCollector
    {

        public string ConnectionString;



        /*
         * 
select i.uidInstanceID, sc.nvcName, i.dtSuspendTimeStamp, i.nErrorCategory, a.nvcApplicationName as 'Hostinstance', btso.nvcName, btso.nvcFullName
from Instances i 
inner join Applications a
on i.uidAppOwnerID = a.uidAppID
inner join ServiceClasses sc
on i.uidClassID = sc.uidServiceClassID
inner join [Services] s
on i.uidServiceID = s.uidServiceID
--inner join Modules m
--on m. = m.nModuleID
left join [BizTalkMgmtDb].[dbo].[bts_orchestration] btso
on i.uidServiceID = btso.uidGUID
--where m.nvcName = 'BorisImport'
order by isnull(nErrorCategory,99), sc.nvcName


         * 
         */


        public List<Instance> CollectHostInstancesFromApplication (string ApplicationName)
        {

            string SelectCmdString =
                @"select 
i.uidInstanceID,
i.nState 
c.nvcName instancetype,
SUBSTRING(Sub.nvcName,9,Charindex('{',Sub.nvcName) - 9) omschrijving,
 a.nvcApplicationName hostname
from Instances i 
inner join Applications a 
	on i.uidAppOwnerID = a.uidAppID 
inner join ServiceClasses c
	on i.uidClassID = c.uidServiceClassID
inner join Services s on
	i.uidServiceID = s.uidServiceID 
inner join Modules m on
	m.nModuleID = s.nModuleID
left join Subscription sub
on i.uidInstanceID = sub.uidInstanceID
where m.nvcName = '" +
                ApplicationName + "';";

            
            List<Instance> instances = new List<Instance>();

           foreach ( DataRow Rij in SelectCmdString.getDtaDataSet(ConnectionString).Tables[ExtensionMethods.BaseTable].Rows)
            {
                instances.Add(new Instance(Rij));

            }

            return instances;

        }


        public List<string> GetHosts(string Hostname)
        {
            
            List<string> hostnames = new List<string>();
            string SelectCmdString = "select * from Modules";
            foreach (DataRow rij in SelectCmdString.getDtaDataSet(ConnectionString).Tables[ExtensionMethods.BaseTable].Rows)
            {
                hostnames.Add(rij["nvcName"].ToString());
            }

            return hostnames;

        }

    }
}
    ;