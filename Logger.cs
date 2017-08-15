using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageExtractor;

namespace SaveMessages
{
    public class Logger: IExceptionLogger
    {


        public void VerwerkExceptionText(string ex)
        {
            MessageBox.Show(ex);
        }
    }
}
