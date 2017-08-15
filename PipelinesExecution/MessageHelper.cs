// reflected code afblijven dus.
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using System;

namespace Microsoft.BizTalk.Component
{
    internal sealed class MessageHelper
    {
        public static void CopyMessageParts(IPipelineContext pc, IBaseMessage inmsg, IBaseMessage outmsg, IBaseMessagePart bodyPart)
        {
            MessageHelper.CopyMessageParts(pc, inmsg, outmsg, bodyPart, false);
        }

        public static void CopyMessageParts(IPipelineContext pc, IBaseMessage inmsg, IBaseMessage outmsg, IBaseMessagePart bodyPart, bool allowUnrecognizeMessage)
        {
            string bodyPartName = inmsg.BodyPartName;
            for (int index = 0; index < inmsg.PartCount; ++index)
            {
                string partName = (string)null;
                IBaseMessagePart partByIndex = inmsg.GetPartByIndex(index, out partName);
                if (partByIndex == null && !allowUnrecognizeMessage)
                    throw new ArgumentNullException("otherOutPart[" + (object)index + "]");
                if (bodyPartName != partName)
                {
                    WPPTrace.WriteLine(WPPTrace.Debug, "CopyMessageParts:\tAttach incoming message parts to the outgoing message: " + partName);
                    outmsg.AddPart(partName, partByIndex, false);
                }
                else
                    outmsg.AddPart(bodyPartName, bodyPart, true);
            }
        }
    }
}
