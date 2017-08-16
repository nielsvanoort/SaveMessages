using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Text;
using MessageExtractor;
using Microsoft.BizTalk.Agent.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
//using SaveMessages.HelperClass;
using System.Runtime.InteropServices;
using SaveMessages.ExtensionMethods;

namespace SaveMessages.MessageExtraction
{
    public class MessageExtracter
    {
        public String ConnectionString { get; set; }
        public IExceptionLogger logger;




        public List<String> getApplications()
        {
            DataCollector collector = new DataCollector(){ConnectionString = this.ConnectionString};
            return collector.GetApplications();

        }




        public void SaveApplicationToFiles(string applicationName, string appDirectory, bool DirectoryForInstanceType, bool DirectoryForInstance, bool DirectoryForInstanceNamespace)
        {
            var x = new DataCollector(){ConnectionString = this.ConnectionString}.CollectInstancesFromApplication(applicationName);
            savetofiles(x,appDirectory,DirectoryForInstanceType,DirectoryForInstance,DirectoryForInstanceNamespace);            

        }

        public void savetofiles(List<Instance> instances, string appDirectory, bool DirectoryForInstanceType, bool DirectoryForInstance, bool DirectoryForInstanceNamespace)
        {

            instances.ForEach(inst =>
            {

                string workingdirectory = appDirectory + @"\" + inst.Application.GetValidDirectoryString(); ;
                if (DirectoryForInstanceType) workingdirectory += @"\" + inst.instancetype.GetValidDirectoryString(); 
                if (DirectoryForInstanceNamespace) workingdirectory += @"\" + inst.omschrijving.GetValidDirectoryString();
                if (DirectoryForInstance) workingdirectory += @"\" + inst.InstanceID.ToString().GetValidDirectoryString();
                //workingdirectory = workingdirectory.GetValidDirectoryString();
                if (!Directory.Exists(workingdirectory)) Directory.CreateDirectory(workingdirectory);
                DataCollector collector = new DataCollector(){ConnectionString = this.ConnectionString};
                foreach ( Message message in collector.getMessagesFromInstance(inst))
                {
                
                 File.WriteAllText(workingdirectory + @"\" + message.Messageid + ".xml", message.Contents  );   
                 File.WriteAllText(workingdirectory + @"\" + message.Messageid + ".cont.xml", message.Context  );   
                }


            });
        }

        public List<Instance> getSuspendedInstances()
        {

            try
            {

                DataCollector collecor = new DataCollector() {ConnectionString = this.ConnectionString};
                List<Instance> antwoord = collecor.CollectInstancesByState(4);
                antwoord.AddRange(collecor.CollectInstancesByState(32));
                return antwoord;
            }
            catch (Exception ex)
            {

                logger.VerwerkExceptionText(ShowAllExceptions(ex));
                throw ex;

            }
        }

        public List<Instance> GetActiveInstances()
        {
            DataCollector collecor = new DataCollector() { ConnectionString = this.ConnectionString };
            List<Instance> antwoord = collecor.CollectInstancesByState(1);
            antwoord.AddRange(collecor.CollectInstancesByState(2));
            antwoord.AddRange(collecor.CollectInstancesByState(8));
            return antwoord;
        }



        public void SaveAndDeleteMessagesFromHost(string hostname, string appDirectory, bool DirectoryForInstanceType, bool DirectoryForInstance, bool DirectoryForInstanceNamespace)
        {
            try
            {
                DataCollector dc = new DataCollector();
                dc.ConnectionString = this.ConnectionString;
                List<Instance> instances;
                List<Message> messages;

                dc.getHostInstanceForFlush(hostname, out instances, out messages);
                savetofiles(instances, appDirectory, DirectoryForInstanceType, DirectoryForInstance, DirectoryForInstanceNamespace);
                Terminator tr = new Terminator();
                tr.connectionstring = ConnectionString;
                instances.ForEach(t => { tr.TerminateInstance(t); });
                DeleteMesagesFromHost(hostname);
            }
            catch (Exception ex)
            {

                logger.VerwerkExceptionText(ShowAllExceptions(ex));

            }

        }




        public void DeleteMesagesFromHost(string hostname)
        {
            try
            {
                string cmd = @"

Select test.uidMessageID
into #remove

from
(
select uidMessageID
from " + hostname + @"_MessageRefCountLog d
union
select uidMessageID from " + hostname + @"Q d
union
select uidMessageID from " + hostname + @"Q_Scheduled d
union
select uidMessageID from " + hostname + @"Q_Suspended d
union
select uidMessageID from InstanceStateMessageReferences_" + hostname + @" d
union
select uidMessageID from InstanceStateMessageReferences_" + hostname + @"IsolatedHost d
) test



delete s from spool s 
where s.[uidMessageID ] in
(select r.uidMessageID from #remove r)


delete s from MessageParts s 
where s.[uidMessageID ] in
(select r.uidMessageID from #remove r)



delete i from Instances i
inner join Applications a on
i.uidAppOwnerID = a.uidAppID
where a.nvcApplicationName like '" + hostname + @"'


delete i from InstancesSuspended i
inner join Applications a on
i.uidAppOwnerID = a.uidAppID
where a.nvcApplicationName like '" + hostname + @"'


truncate table " + hostname + @"_DequeueBatches
truncate table " + hostname + @"_MessageRefCountLog
truncate table " + hostname + @"Q
truncate table " + hostname + @"Q_Scheduled
truncate table " + hostname + @"Q_Suspended
truncate table DynamicStateInfo_" + hostname + @"
truncate table DynamicStateInfo_" + hostname + @"IsolatedHost
truncate table InstanceStateMessageReferences_" + hostname + @"
truncate table InstanceStateMessageReferences_" + hostname + @"IsolatedHost
";

               cmd.getDtaDataSet(ConnectionString);

            }
            catch (Exception ex)
            {
                
                logger.VerwerkExceptionText(ShowAllExceptions(ex));

            }

        }




        public string ShowAllExceptions(Exception ex)
        {
            if (ex.InnerException != null) ShowAllExceptions(ex.InnerException);
            return ex.Message + "\n" + ex.StackTrace + "\n";


        }

    }
}
