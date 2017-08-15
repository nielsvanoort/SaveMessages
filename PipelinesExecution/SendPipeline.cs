// Decompiled with JetBrains decompiler
// Type: Microsoft.Test.BizTalk.PipelineObjects.SendPipeline
// Assembly: PipelineObjects, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: A7DE365F-B06A-4F17-BA46-B7ED2FF6647C
// Assembly location: C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\PublicAssemblies\PipelineObjects.dll

using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Test.BizTalk.PipelineObjects;

using SaveMessages.PipelinesExecution;
using System;

namespace SaveMessages.Pipeline
{


    // gereflecteerde code uit de microsoft dll
    // vanaf blijven dus
    public class SendPipeline : GenericPipeline
    {

        //Extended MessageContextToegevoegd om de pipelinecomponent properties te kunnen kopieren naar de messagecontext
        public ExtendedMessageContext CopyContext = new ExtendedMessageContext();

        private IBaseMessage outputMessage;

        internal SendPipeline(Guid categoryId)
            : base(categoryId)
        {
        }

        public override void Execute(IPipelineContext pipelineContext)
        {
            if (pipelineContext == null)
                throw new ArgumentNullException("pipelineContext");
            if (this.InputMessages.Count == 0)
                throw new InvalidOperationException("There must be an input message for the send pipeline");
            int assemblingStageIndex = this.GetAssemblingStageIndex();
            if (assemblingStageIndex == -1)
                throw new InvalidOperationException("Assembling stage is not found");
            Stage stage = this.Stages[assemblingStageIndex] as Stage;
            bool severalMessages = this.InputMessages.Count > 1;
            foreach (IBaseMessage inputMessage in this.InputMessages)
            {
                this.outputMessage = this.ExecuteSubPipeline(pipelineContext, inputMessage, 0, assemblingStageIndex - 1);
                this.FireCalling((object)stage, "AddMessage");
                stage.AddMessage(pipelineContext, this.outputMessage, severalMessages);
                this.FireCalled((object)stage, "AddMessage");
            }
            this.FireCalling((object)stage, "Assemble");
            this.outputMessage = stage.Assemble(pipelineContext);
            this.FireCalled((object)stage, "Assemble");
            this.outputMessage = this.ExecuteSubPipeline(pipelineContext, this.outputMessage, assemblingStageIndex + 1, this.Stages.Count - 1);
        }

        public override IBaseMessage GetNextOutputMessage(IPipelineContext pipelineContext)
        {
            IBaseMessage baseMessage = this.outputMessage;
            this.outputMessage = (IBaseMessage)null;
            return baseMessage;
        }

        private int GetAssemblingStageIndex()
        {
            for (int index = 0; index < this.Stages.Count; ++index)
            {
                if ((this.Stages[index] as Stage).IsAssemblingStage())
                    return index;
            }
            throw new InvalidOperationException("Assembling stage is not found");
        }
    }
}
