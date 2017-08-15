using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMessages.MessageExtraction
{
    public enum MessageState
    {
        None = 0, 
        Started  = 1,
        Completed = 2 ,
        Terminated = 3,
        Suspended = 4 ,
        ReadyToRun = 5  ,
        Active =6 ,
        Dehydrated =8 ,
        CompletedWithDiscardedMessages =16 ,
        SuspendedNonResumable =32 ,
        InBreakpoint=64
    }
}
