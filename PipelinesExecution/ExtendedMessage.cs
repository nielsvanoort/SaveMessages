using System.IO;
using Microsoft.Test.BizTalk.PipelineObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BizTalk.Message.Interop;

namespace SaveMessages.PipelinesExecution
{
    public static class ExtendedMessage
    {

        public static void addContextFromFile(this IBaseMessage m,  string file)
        {
            m.Context = new ExtendedMessageContext();

            //vreselijke hack om te testen
           FileInfo fx = new FileInfo(file);
           m.Context.Write("OutboundTransportLocation", PipelineConstants.SaveMessageNamespace, fx.Directory + @"\ExecutedMessages\%SourceFileName%");
            string[] items = File.ReadAllLines(file);
            foreach (var Item in items)
            {
                string[] a = Item.Split(';');
                if (a.Count() > 0) m.Context.Write(a[0], PipelineConstants.SaveMessageNamespace , a[1].Trim() );
            }
            
        }

    }





}
