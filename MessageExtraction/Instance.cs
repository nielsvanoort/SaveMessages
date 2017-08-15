using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMessages.MessageExtraction
{

    public class Instance
    {
        private readonly DataRow _values;

        public Instance(DataRow values)
        {

            _values = values;

        }


        public Guid InstanceID
        {
            get { return Guid.Parse(_values["uidInstanceID"].ToString()); }
        }


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


    }
}
