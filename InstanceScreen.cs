using System.Runtime.Remoting.Messaging;
using SaveMessages.Actions;
using SaveMessages.HelperClass;
using SaveMessages.MessageExtraction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = SaveMessages.MessageExtraction.Message;

namespace SaveMessages
{
    public partial class InstanceScreen : Form
    {
        private Instance _instance;
        public string Connectionstring;
        public InstanceScreen()
        {
            InitializeComponent();
        }

        public InstanceScreen(Instance instance, string connectionstring)
        {

            Connectionstring = connectionstring;
            InitializeComponent();
            _instance = instance;
            lblInstanceID.Text += _instance.InstanceID.ToString();
            lblApplication.Text += instance.Application ?? "";
            lblhostname.Text += _instance.hostname ?? "";
            lblinstancetype.Text += _instance.instancetype ?? "";
            lblstate.Text += _instance.state ?? "";
            
            DataCollector collector = new DataCollector(){ConnectionString = this.Connectionstring};
            foreach (Message msg in collector.getMessagesFromInstance(instance))
            {

                tvMessages.Nodes.Add(new ExtendedTreeNode(msg.MessageType +"("+ instance.state + ")") {TheMessage = msg});
            }

        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {

            BizTalkActionHandler action = new BizTalkActionHandler()
            {
                ConnectionString = this.Connectionstring
            };

            action.Suspend(_instance);
        }

        private void btnResume_Click(object sender, EventArgs e)
        {

            BizTalkActionHandler action = new BizTalkActionHandler()
            {
                ConnectionString = this.Connectionstring
            };

            action.Resume(_instance);
        }

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            BizTalkActionHandler action = new BizTalkActionHandler()
            {
                ConnectionString = this.Connectionstring
            };

            action.Terminate(_instance);

        }

        private void tvMessages_Click(object sender, EventArgs e)
        {

        }

        private void tvMessages_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        private void ShowMessage()
        {
            if (tvMessages.SelectedNode != null)
            {
                MessageScreen screen = new MessageScreen(((ExtendedTreeNode)tvMessages.SelectedNode).TheMessage);
                screen.ShowDialog();
            }
        }

        private void tvMessages_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode.ToString().ToLower() == "return")
            {
                ShowMessage();
            }
        }


    }
}
