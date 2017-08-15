using Microsoft.BizTalk.Message.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMessages.PipelinesExecution
{
    public static class ExtensionMethods
    {

        //methode om onder andere private properties op te halen op basis van reflectie

        public static object getProperty(this object theobject, string propertyname)
        {
            try
            {
                Type type = theobject.GetType();
                return type.GetProperties().First(c => c.Name == propertyname).GetValue(theobject, null);
            }
            catch
            {
                return null;
            }
        }

        //splits de contextfile in keyvalue pairs en voegt deze toe aan de messagecontext
        public static void addContextFromFile(this IBaseMessage message, string file, string outpath)
        {
            message.Context = new ExtendedMessageContext();
            message.Context.Write("OutboundTransportLocation", PipelineConstants.SaveMessageNamespace, outpath + @"\%SourceFileName%");
            string[] items = File.ReadAllLines(file);
            foreach (var Item in items)
            {
                string[] a = Item.Split(';');
                if (a.Count() > 0) message.Context.Write(a[0], PipelineConstants.SaveMessageNamespace, a[1].Trim());
            }

        }

    }
}
