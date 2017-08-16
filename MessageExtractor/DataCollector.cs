using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.XLANGs.BaseTypes.DataSetStandIn;
using System.Data;
using ErrorReport;
using SaveMessages.ExtensionMethods;

namespace SaveMessages.MessageExtraction
{
    public class DataCollector
    {

        public string ConnectionString;

        public Message GetMessageByID(string messageid, string instanceid)
        {

            string SelectCmdString =

             @"select * from 
            MessageParts mp
            inner join Parts p
            on mp.uidPartID = p.uidPartID
            inner join spool s
            on S.[uidMessageID ] = mp.[uidMessageID ]
            left join Fragments f
            on mp.uidPartID = f.uidPartID
            where mp.[uidMessageID ] = '" + messageid + @"'
            order by nFragmentNumber";


            return new Message(SelectCmdString.getDtaDataSet(ConnectionString).Tables[Methods.BaseTable].toRowList(), messageid, instanceid);
           
        }



        public List<Message> getMessagesFromInstance(Instance inst)
        {
            
            string SelectCmdString = @"Select test.uidMessageID
            from
            (
            select uidMessageID
            from "+inst.hostname+@"_MessageRefCountLog d
            where d.uidInstanceID = '"+inst.InstanceID+@"'
            union
            select uidMessageID from "+inst.hostname+@"Q d
            where d.uidInstanceID = '"+inst.InstanceID+@"'
            union
            select uidMessageID from "+inst.hostname+@"Q_Scheduled d
            where d.uidInstanceID = '"+inst.InstanceID+@"'
            union
            select uidMessageID from "+inst.hostname+@"Q_Suspended d
            where d.uidInstanceID = '"+inst.InstanceID+@"'
            union
            select uidMessageID from InstanceStateMessageReferences_"+inst.hostname+@" d
            where d.uidInstanceID = '"+inst.InstanceID+@"'
            ) test
            ";


            List<Message> berichten = new List<Message>();
            foreach (DataRow Message in SelectCmdString.getDtaDataSet(ConnectionString).Tables[Methods.BaseTable].Rows)
            {
                berichten.Add(GetMessageByID(Message["uidMessageID"].ToString(), inst.InstanceID.ToString()));
            }
            return berichten;
        }



        public List<Instance> CollectInstancesFromApplication (string ApplicationName)
        {

            string SelectCmdString =
                @"select 
                i.uidInstanceID,
                i.nState, 
                c.nvcName instancetype,
                isnull (SUBSTRING(Sub.nvcName,9,Charindex('{',Sub.nvcName) - 9), 'none') omschrijving,
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
                ApplicationName + "'";

            
            List<Instance> instances = new List<Instance>();

           foreach ( DataRow Rij in SelectCmdString.getDtaDataSet(ConnectionString).Tables[Methods.BaseTable].Rows)
            {
                instances.Add(new Instance(Rij,ApplicationName,this));

            }

            return instances;

        }


        public Instance getInstanceById(string instanceid)
 {

            string SelectCmdString =
                @"select 
                i.uidInstanceID,
                i.nState, 
                c.nvcName instancetype,
                SUBSTRING(Sub.nvcName,9,Charindex('{',Sub.nvcName) - 9) omschrijving,
                 a.nvcApplicationName hostname,
                m.nvcName appname
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
                where i.uidInstanceID  = '" +
                instanceid + "'";


            List<Instance> instances = new List<Instance>();

            foreach (DataRow Rij in SelectCmdString.getDtaDataSet(ConnectionString).Tables[Methods.BaseTable].Rows)
            {
                instances.Add(new Instance(Rij, Rij["appname"].ToString(),this));

            }

            return instances.FirstOrDefault();

        }

        public List<Instance> CollectInstancesByState(int state)
        {

            string SelectCmdString =
                @"select 
                i.uidInstanceID,
                i.nState, 
                c.nvcName instancetype,
                SUBSTRING(Sub.nvcName,9,Charindex('{',Sub.nvcName) - 9) omschrijving,
                 a.nvcApplicationName hostname,
                m.nvcName appname
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
                where i.nState  = '" +
                state + "'";


            List<Instance> instances = new List<Instance>();

            foreach (DataRow Rij in SelectCmdString.getDtaDataSet(ConnectionString).Tables[Methods.BaseTable].Rows)
            {
                instances.Add(new Instance(Rij, Rij["appname"].ToString(),this));

            }

            return instances;

        }



        public List<string> GetApplications()
        {
            
            List<string> hostnames = new List<string>();
            string SelectCmdString = "select * from Modules";
            foreach (DataRow rij in SelectCmdString.getDtaDataSet(ConnectionString).Tables[Methods.BaseTable].Rows)
            {
                hostnames.Add(rij["nvcName"].ToString());
            }

            return hostnames;

        }




        public void getHostInstanceForFlush(string hostinstanceName, out List<Instance> instances, out List<Message> messages )
        {

            string selectcmd = @"Select test.uidMessageID, uidInstanceID
from
(
select uidMessageID, uidInstanceID
from " + hostinstanceName + @"_MessageRefCountLog d
union
select uidMessageID, uidInstanceID
from " + hostinstanceName + @"Q d
union
select uidMessageID, uidInstanceID
from " + hostinstanceName + @"Q_Scheduled d
union
select uidMessageID, uidInstanceID
from " + hostinstanceName + @"Q_Suspended d
union
select uidMessageID, uidInstanceID
from InstanceStateMessageReferences_" + hostinstanceName + @" d
union
select uidMessageID, uidInstanceID
from InstanceStateMessageReferences_" + hostinstanceName + @"IsolatedHost d
) test";

       List<DataRow>  rijlist =  selectcmd.getDtaDataSet(ConnectionString).Tables[Methods.BaseTable].Rows.ToList();

                instances =
                (
                    from rij in  rijlist 
                    where rij["uidInstanceID"].ToString().GetType() != typeof(DBNull)
                    select getInstanceById(rij["uidInstanceID"].ToString())
                    ).ToList();

                messages = (
                        from rij in rijlist
                        where rij["uidInstanceID"].ToString().GetType() == typeof(DBNull)
                        select GetMessageByID(rij["uidMessageID"].ToString(),"")
                        ).ToList();



        }



    }
}
    