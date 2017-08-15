using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Test.BizTalk.PipelineObjects;

namespace SaveMessages.PipelinesExecution
{
    public class ExtendedMessageContext:MessageContext, IBaseMessageContext
    {
        
        // bij het opslaan uit de database gaat de namespace verloren.
        // Hierdoor moet alles opgehaald worden met de PipelineConstants namespace (bij aanroep uit de random pipeline)

        public object Read(string name, string namesp)
        {
            return base.Read(name, PipelineConstants.SaveMessageNamespace);
        }

    }
}
