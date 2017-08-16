using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMessages.MessageExtraction;
using SaveMessages.ExtensionMethods;
namespace MessageExtractor
{
    public class Terminator
    {
        public string connectionstring;

        public void TerminateInstance(Instance inst)
        {
            foreach (Message message in inst.Messages)
            {
                TerminateMessage(message, inst.hostname);
            }

            string cmd =@"delete from Instances where uidInstanceID = '" + inst.InstanceID.ToString() + @"'
              delete from InstancesSuspended where uidInstanceID = '" + inst.InstanceID.ToString() +
            @"'";
            cmd.getDtaDataSet(connectionstring);

        }

        public void TerminateMessage(Message mes, string hostname)
        {

           string cmd = @"delete d
from " + hostname + @"_MessageRefCountLog d
where uidMessageID = '" + mes.Messageid + @"'

delete d
from " + hostname + @"Q d
where uidMessageID = '" + mes.Messageid + @"'

delete d
from " + hostname + @"Q_Scheduled d
where uidMessageID = '" + mes.Messageid + @"'

delete d
from " + hostname + @"Q_Suspended d
where uidMessageID = '" + mes.Messageid + @"'

delete d
from InstanceStateMessageReferences_" + hostname + @" d
where uidMessageID = '" + mes.Messageid + @"'

delete d
from InstanceStateMessageReferences_" + hostname + @"IsolatedHost d
where uidMessageID = '" + mes.Messageid + @"'

delete s from spool s 
where uidMessageID = '" + mes.Messageid + @"'

delete p from parts p 
inner join messageparts mp
on mp.uidPartID = p.uidPartID
where uidMessageID = '" + mes.Messageid + @"'
					

delete s from MessageParts s 
where uidMessageID = '" + mes.Messageid + @"'
";

           cmd.getDtaDataSet(connectionstring);

        }
    }
}
