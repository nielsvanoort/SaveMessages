using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS;

namespace SaveMessages.MessageExtraction
{

    public class Instance
    {
        private readonly DataRow _values;
        private DataCollector Collector;

        public Instance(DataRow values, string application, DataCollector collector)
        {
            Collector = collector;
            Application = application;
            _values = values;

        }


        public Guid InstanceID
        {
            get { return Guid.Parse(_values["uidInstanceID"].ToString()); }
        }


        public string Application;

        public string state {
            get
            {
                switch (int.Parse(_values["nState"].ToString()))
                {
                    case 1:
                        return "Ready To Run";
                    case 2:
                        return "Active";
                    case 4 :
                        return "Suspended Resumable";
                    case 8 :
                        return "Dehydrated";
                    case 16 :
                        return "Completed With Discarded Messages";
                    case 32 :
                        return "Suspended Non-Resumable";
                }

                return "";
            }
        }

         public string instancetype {
            get { return _values["instancetype"].ToString(); }
        }

                 public string omschrijving {
            get { return _values["omschrijving"].ToString(); }
        }

                 public string hostname {
            get { return _values["hostname"].ToString(); }
        }

        public override string ToString()
        {
            if (! String.IsNullOrWhiteSpace(omschrijving)) return omschrijving;
            Message firstMessage = Messages.FirstOrDefault();
            if (firstMessage != null )
                {
                if (!String.IsNullOrWhiteSpace(firstMessage.MessageType)) return firstMessage.MessageType;
                if (!String.IsNullOrWhiteSpace(firstMessage.ContextValues.Get("ReceiveLocationName")))
                    return firstMessage.ContextValues.Get("ReceiveLocationName");

                if (!String.IsNullOrWhiteSpace(firstMessage.ContextValues.Get("ReceivePortName")))
                    return firstMessage.ContextValues.Get("ReceivePortName");
                }
                 
            return  InstanceID.ToString();
        }

        private List<Message> _messages;

        public List<Message> Messages
        {
            get
            {
                _messages = _messages ??  Collector.getMessagesFromInstance(this);
                return _messages;
            }

            set {  _messages = value; }

        }

    }
}
