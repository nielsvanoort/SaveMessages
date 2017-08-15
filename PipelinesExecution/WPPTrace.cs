//BizTalk gebruikt voor een helehoop events die je niet wil weten de WPPTraceTool. 
//Mocht je deze niet beschikbaar hebben op je omgeving of benaderbaar vanuit de applicatie zorgt deze voor een bak een comexeptions.
//Dit reroute al je message naar het console.

// Decompiled with JetBrains decompiler
// Type: Microsoft.BizTalk.Component.WPPTrace
// Assembly: Microsoft.BizTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: B1CFBF4F-AD28-444B-8CBF-7BFD06FCFB2D
// Assembly location: D:\BizTalk 2013\Pipeline Components\Microsoft.BizTalk.Pipeline.Components.dll

//using Microsoft.BizTalk.Diagnostics;
using System;
using System.Diagnostics;

namespace Microsoft.BizTalk.Component
{
    internal class WPPTrace
    {
        public static readonly uint Error = 1U;
        public static readonly uint Warning = 2U;
        public static readonly uint Info = 4U;
        public static readonly uint Debug = 512U;
        private static readonly Guid providerGuid = new Guid("{689E4209-67A2-409b-8303-0905CE67A931}");
        private static readonly string providerName = "Microsoft Biztalk Component";
        

        public static void WriteLine(uint Flags, string format)
        {

            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(Flags, (object)format);
        }

        public static void WriteLine(uint Flags, string format, object data1)
        {
            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(Flags, (object)format, data1);
        }

        public static void WriteLine(uint Flags, string format, object data1, object data2)
        {
            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(Flags, (object)format, data1, data2);
        }

        public static void WriteLine(uint Flags, string format, object data1, object data2, object data3)
        {
            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(Flags, (object)format, data1, data2, data3);
        }

        public static void WriteLine(uint Flags, string format, object data1, object data2, object data3, object data4)
        {
            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(Flags, (object)format, data1, data2, data3, data4);
        }

        [Conditional("DEBUG")]
        public static void DebugTrace(string format)
        {
            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(WPPTrace.Debug, (object)format);
        }

        [Conditional("DEBUG")]
        public static void DebugTrace(string format, object data1)
        {
            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(WPPTrace.Debug, (object)format, data1);
        }

        [Conditional("DEBUG")]
        public static void DebugTrace(string format, object data1, object data2)
        {
            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(WPPTrace.Debug, (object)format, data1, data2);
        }

        [Conditional("DEBUG")]
        public static void DebugTrace(string format, object data1, object data2, object data3)
        {
            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(WPPTrace.Debug, (object)format, data1, data2, data3);
        }

        [Conditional("DEBUG")]
        public static void DebugTrace(string format, object data1, object data2, object data3, object data4)
        {
            Console.WriteLine(format);
            //if (WPPTrace.tracer == null)
            //    return;
            //WPPTrace.tracer.TraceMessage(WPPTrace.Debug, (object)format, data1, data2, data3, data4);
        }
    }
}
