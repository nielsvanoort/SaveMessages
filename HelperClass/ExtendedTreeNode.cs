using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaveMessages.MessageExtraction;

namespace SaveMessages.HelperClass
{
    public class ExtendedTreeNode:TreeNode
    {
        public Instance theInstance;
        public MessageExtraction.Message TheMessage;

        public  ExtendedTreeNode(string name):base(name)
        {

        }


    }
}
