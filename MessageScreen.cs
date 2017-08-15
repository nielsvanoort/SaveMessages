using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveMessages
{
    public partial class MessageScreen : Form
    {
        public MessageScreen()
        {
            InitializeComponent();
        }

        public MessageScreen(MessageExtraction.Message msg)
        {
            InitializeComponent();
            tbProps.Lines = msg.Context.Split('\n');
            tbMessage.Text = msg.Contents;

        }

    }
}
