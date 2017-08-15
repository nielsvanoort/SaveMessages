// gereflecte variant van de pipelinecomponents dll. met wat aanpassingen om het geheel te laten draaien binnen de tool
// aanpassingen met comments erboven
// Deze is in eerste instantie gereflect omdat er ergens een Com exception vervangen is door een nietszeggende.
// mogelijk in een volgende versie switch maken waarmee je de originele ook kan gebruiken


// Decompiled with JetBrains decompiler
// Type: Microsoft.BizTalk.Component.FFAsmComp
// Assembly: Microsoft.BizTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: B1CFBF4F-AD28-444B-8CBF-7BFD06FCFB2D
// Assembly location: D:\BizTalk 2013\Pipeline Components\Microsoft.BizTalk.Pipeline.Components.dll

using Microsoft.BizTalk.Component;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Component.Utilities;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Streaming;
using Microsoft.XLANGs.BaseTypes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using XMLNORM;

namespace SaveMessages.Pipeline
{
    [ComponentCategory("9d0e4107-4cce-4536-83fa-4a5040674ad6")]
    [ComponentCategory("9d0e410c-4cce-4536-83fa-4a5040674ad6")]
    [Guid("6469e38f-712d-4086-9795-78103ab670f4")]
    [ComponentCategory("9d0e4100-4cce-4536-83fa-4a5040674ad6")]
    public class FFAsmComp : BaseCustomTypeDescriptor, IBaseComponent, IAssemblerComponent, IPersistPropertyBag, IComponentUI
    {
        private static ResourceManager resManager = new ResourceManager("Microsoft.BizTalk.Component.FFAssembler.Resource", typeof(FFAsmComp).Assembly);
        private static PropertyBase m_headerDocumentProperty = (PropertyBase)new FlatFileHeaderDocument();
        private static PropertyBase m_headerSpecNameProperty = (PropertyBase)new HeaderSpecName();
        private static PropertyBase m_documentSpecNameProperty = (PropertyBase)new DocumentSpecName();
        private static PropertyBase m_trailerSpecNameProperty = (PropertyBase)new TrailerSpecName();
        private static PropertyBase m_sourceCharsetProperty = (PropertyBase)new SourceCharset();
        private static PropertyBase m_targetCharsetProperty = (PropertyBase)new TargetCharset();
        private static PropertyBase processingByteOrderMark = (PropertyBase)new PreserveBom();
        private ArrayList m_xmlAsmComps = new ArrayList();
        private Encoding m_Encoding = Encoding.UTF8;
        private SchemaWithNone headerSpecName = new SchemaWithNone(string.Empty);
        private SchemaWithNone documentSpecName = new SchemaWithNone(string.Empty);
        private SchemaWithNone trailerSpecName = new SchemaWithNone(string.Empty);
        private string strCharset = string.Empty;
        private CharsetList charset = new CharsetList(string.Empty, 0);
        private IBaseMessage m_inMsg;
        private ArrayList m_inMsgs;
        private string m_headerDocument;
        private string m_targetCharset;
        private string m_sourceCharset;
        private string m_headerSpecName;
        private string m_documentSpecName;
        private string m_trailerSpecName;
        private bool m_preserveBom;
        private string m_pipelinecontextHeaderSpecName;
        private string m_pipelinecontextDocumentSpecName;
        private string m_pipelinecontextTrailerSpecName;
        private int codepage;

        [BtsPropertyName("PropFFAsmName")]
        [BtsDescription("DescFFAsmName")]
        [Browsable(false)]
        public string Name
        {
            get
            {
                return FFAsmComp.resManager.GetString("FlatfileAssembler");
            }
        }

        [Browsable(false)]
        [BtsPropertyName("PropFFAsmVersion")]
        [BtsDescription("DescFFAsmVersion")]
        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        [BtsPropertyName("PropFFAsmDescription")]
        [BtsDescription("DescFFAsmDesription")]
        [Browsable(false)]
        public string Description
        {
            get
            {
                return FFAsmComp.resManager.GetString("FlatfileAssemblerDescription");
            }
        }

        [BtsDescription("DescHeaderSpecName")]
        [BtsPropertyName("PropHeaderSpecName")]
        public SchemaWithNone HeaderSpecName
        {
            get
            {
                return this.headerSpecName;
            }
            set
            {
                this.headerSpecName = value;
                this.m_pipelinecontextHeaderSpecName = this.headerSpecName.SchemaName;
                this.m_headerSpecName = this.m_pipelinecontextHeaderSpecName;
            }
        }

        [BtsPropertyName("PropDocumentSpecName")]
        [BtsDescription("DescDocumentSpecName")]
        public SchemaWithNone DocumentSpecName
        {
            get
            {
                return this.documentSpecName;
            }
            set
            {
                this.documentSpecName = value;
                this.m_pipelinecontextDocumentSpecName = this.documentSpecName.SchemaName;
                this.m_documentSpecName = this.m_pipelinecontextDocumentSpecName;
            }
        }

        [BtsDescription("DescTrailerSpecName")]
        [BtsPropertyName("PropTrailerSpecName")]
        public SchemaWithNone TrailerSpecName
        {
            get
            {
                return this.trailerSpecName;
            }
            set
            {
                this.trailerSpecName = value;
                this.m_pipelinecontextTrailerSpecName = this.trailerSpecName.SchemaName;
                this.m_trailerSpecName = this.m_pipelinecontextTrailerSpecName;
            }
        }

        [BtsPropertyName("PropTargetCharSetText")]
        [BtsDescription("DescTargetCharSetText")]
        public CharsetList TargetCharset
        {
            get
            {
                return this.charset;
            }
            set
            {
                this.charset = value;
                this.strCharset = this.charset.Name;
                this.codepage = this.charset.Codepage;
            }
        }

        [BtsDescription("DescPreserveBom")]
        [BtsPropertyName("PropPreserveBom")]
        public bool PreserveBom
        {
            get
            {
                return this.m_preserveBom;
            }
            set
            {
                this.m_preserveBom = value;
            }
        }

        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return ResourceHandler.GetResourceBitmap(FFAsmComp.resManager, "AssemblerBitmap").GetHicon();
            }
        }

        public FFAsmComp()
            : base(FFAsmComp.resManager)
        {
        }

        public void AddDocument(IPipelineContext pc, IBaseMessage inMsg)
        {
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp:AddDocument:\tEntered");
            try
            {
                this.AddDocument2(pc, inMsg);
            }
            catch (FFAsmException ex)
            {
                ex.StoreException(pc, this.Name);
                throw ex;
            }
            catch (ComponentException ex)
            {
                ex.StoreException(pc, this.Name);
                throw ex;
            }
            catch (Exception ex)
            {
                ComponentException.StoreException(pc, ex, this.Name);
                throw ex;
            }
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.AddDocument:\tExiting");
        }

        private void AddDocument2(IPipelineContext pc, IBaseMessage inMsg)
        {
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp:AddDocument2:\tEntered");
            if (pc == null)
                throw new ArgumentNullException("pc");
            if (inMsg == null)
                throw new ArgumentNullException("inMsg");
            if (inMsg.BodyPart == null)
            {
                WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Disassemble2:\tMissing MessageBodyPart");
                throw new FFAsmException(3233813557U, FFAsmComp.resManager.GetString("BTS_FLATFILE_ASSEMBLER_ERROR_NO_BODY_PART"));
            }
            else if (inMsg.BodyPart.GetOriginalDataStream() == null)
            {
                WPPTrace.WriteLine(WPPTrace.Debug, "FFDasmComp.Disassemble2:\tBody part is null.");
                throw new FFAsmException(3233813605U, FFAsmComp.resManager.GetString("BTS_FLATFILE_ASSEMBLER_ERROR_BODY_PART_NULL"));
            }
            else
            {
                if (this.m_inMsgs == null)
                    this.m_inMsgs = new ArrayList();
                this.m_inMsgs.Add((object)inMsg);
                WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.AddDocument2:\tExiting");
            }
        }

        public IBaseMessage Assemble(IPipelineContext pc)
        {
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp:Assemble:\tEntered");
            IBaseMessage baseMessage;
            try
            {
                this.m_inMsg = (IBaseMessage)this.m_inMsgs[0];
                IBaseMessageContext context = this.m_inMsg.Context;
                this.m_headerDocument = (string)context.Read(FFAsmComp.m_headerDocumentProperty.Name.Name, FFAsmComp.m_headerDocumentProperty.Name.Namespace);
                string str1 = (string)context.Read(FFAsmComp.m_targetCharsetProperty.Name.Name, FFAsmComp.m_targetCharsetProperty.Name.Namespace);
                if (str1 != null && str1.Length > 0)
                    this.m_targetCharset = str1;
                this.m_sourceCharset = (string)context.Read(FFAsmComp.m_sourceCharsetProperty.Name.Name, FFAsmComp.m_sourceCharsetProperty.Name.Namespace);
                string str2 = (string)context.Read(FFAsmComp.m_headerSpecNameProperty.Name.Name, FFAsmComp.m_headerSpecNameProperty.Name.Namespace);
                if (str2 != null && str2.Length != 0)
                    this.m_headerSpecName = str2;
                string str3 = (string)context.Read(FFAsmComp.m_documentSpecNameProperty.Name.Name, FFAsmComp.m_documentSpecNameProperty.Name.Namespace);
                if (str3 != null && str3.Length != 0)
                    this.m_documentSpecName = str3;
                string str4 = (string)context.Read(FFAsmComp.m_trailerSpecNameProperty.Name.Name, FFAsmComp.m_trailerSpecNameProperty.Name.Namespace);
                if (str4 != null && str4.Length != 0)
                    this.m_trailerSpecName = str4;
                object obj = context.Read(FFAsmComp.processingByteOrderMark.Name.Name, FFAsmComp.processingByteOrderMark.Name.Namespace);
                if (obj != null)
                    this.m_preserveBom = (bool)obj;
                baseMessage = this.Assemble2(pc);
            }
            catch (FFAsmException ex)
            {
                ex.StoreException(pc, this.Name);
                throw ex;
            }
            catch (ComponentException ex)
            {
                ex.StoreException(pc, this.Name);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Assemble:\tExiting");
            return baseMessage;
        }

        private IBaseMessage Assemble2(IPipelineContext pc)
        {
            string str = string.Empty;
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp:Assemble2:\tEntered");
            IBaseMessageFactory messageFactory = pc.GetMessageFactory();
            IBaseMessage message = messageFactory.CreateMessage();
            message.Context = this.m_inMsg.Context;
            IBaseMessagePart bodyPart = this.m_inMsg.BodyPart;
            IBaseMessagePart messagePart = messageFactory.CreateMessagePart();
            messagePart.PartProperties = PipelineUtil.CopyPropertyBag(bodyPart.PartProperties, messageFactory);
            if (this.m_targetCharset != null && this.m_targetCharset.Length > 0)
            {
                WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Assemble2:\tSet the encoding using the targetCharset XMLNorm context property.");
                this.m_Encoding = Encoding.GetEncoding(this.m_targetCharset);
            }
            else
            {
                IFFDocumentSpec ffDocumentSpec;
                if (this.m_documentSpecName != null && this.m_documentSpecName.Length != 0)
                {
                    WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Assemble2:\tLoading the DocSpec by name.");
                    str = string.Empty;
                    try
                    {
                        ffDocumentSpec = (IFFDocumentSpec)pc.GetDocumentSpecByName(this.m_documentSpecName);
                    }
                    catch (COMException ex)
                    {
                      // betere Heras Hekwerk om de originele foutmeldingen te kunnen zien
                      //  throw new FFAsmException(3233814567U, ComponentException.FormatComExceptionCode(ex), new string[1]
                      //{
                      //  this.m_documentSpecName
                      //});

                        throw ex;
                    }
                }
                else
                {
                    Stream originalDataStream = bodyPart.GetOriginalDataStream();
                    MarkableForwardOnlyEventingReadStream stm = originalDataStream as MarkableForwardOnlyEventingReadStream;
                    if (stm == null)
                    {
                        stm = new MarkableForwardOnlyEventingReadStream(originalDataStream);
                        bodyPart.Data = (Stream)stm;
                    }
                    string docType = Utils.GetDocType(stm);
                    WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Assemble2:\tLoading the DocSpec by type.");
                    str = string.Empty;
                    try
                    {
                        ffDocumentSpec = (IFFDocumentSpec)pc.GetDocumentSpecByType(docType);
                    }
                    catch (COMException ex)
                    {
                        // betere Heras Hekwerk om de originele foutmeldingen te kunnen zien
                        //throw new FFAsmException(3233814566U, ComponentException.FormatComExceptionCode(ex), new string[1]
                        //{
                        //  docType
                        //});
                        throw ex;
                    }
                }
                if (ffDocumentSpec != null)
                {
                    if (ffDocumentSpec.GetDocSchema() != null && ffDocumentSpec.GetDocSchema().CodePage != null)
                    {
                        WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Assemble2:\tSet the encoding using the codepage in the schema.");
                        this.m_Encoding = Encoding.GetEncoding(int.Parse(ffDocumentSpec.GetDocSchema().CodePage, (IFormatProvider)CultureInfo.CurrentCulture));
                    }
                    else if (this.m_sourceCharset != null)
                        this.m_Encoding = Encoding.GetEncoding(this.m_sourceCharset);
                }
            }
            messagePart.Charset = this.m_Encoding.WebName;
            messagePart.ContentType = "text/plain";
            if (!this.m_preserveBom)
                this.m_Encoding = (Encoding)new NoBomEncoding(this.m_Encoding);
            for (int index = 0; index < this.m_inMsgs.Count; ++index)
            {
                FFAsmXMLAsmWrapper asmXmlAsmWrapper = new FFAsmXMLAsmWrapper(Encoding.UTF8.WebName);
                Encoding encoding = ((IBaseMessage)this.m_inMsgs[index]).BodyPart.Charset == null ? Encoding.UTF8 : Encoding.GetEncoding(((IBaseMessage)this.m_inMsgs[index]).BodyPart.Charset);
                asmXmlAsmWrapper.TargetCharset = new CharsetList(encoding.WebName, encoding.CodePage);
                asmXmlAsmWrapper.AddDocument(pc, (IBaseMessage)this.m_inMsgs[index]);
                this.m_xmlAsmComps.Add((object)asmXmlAsmWrapper);
            }

            // op basis van reflectie doorgaan zodat niet alle biztalk dll's opnieuwe gegenereerd moeten worden.
            Type itype = Type.GetType("Microsoft.BizTalk.Component.FFDocumentStream, Microsoft.BizTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", true);
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            CultureInfo culture = null; // use InvariantCulture or other if you prefer

            object[] test =
            new object[]
            {

                pc,
                this.m_xmlAsmComps,
                this.m_headerSpecName,
                this.m_documentSpecName,
                this.m_trailerSpecName,
                this.m_Encoding,
                this.m_headerDocument,
                this.m_inMsg
            };
           
            
            
            ConstructorInfo cinfo = itype.GetConstructor(new []
            {
                typeof(IPipelineContext),
                typeof(ArrayList),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(Encoding),
                typeof(string),
                typeof(IBaseMessage)

            });

            Stream stream = (Stream) cinfo.Invoke(test);
            // orginele aanroep vanuit de dll hieronder, mocht er wat nodig zijn voor debugging purposes
            //Stream stream = (Stream)new FFDocumentStream(pc, this.m_xmlAsmComps, this.m_headerSpecName, this.m_documentSpecName, this.m_trailerSpecName, this.m_Encoding, this.m_headerDocument, this.m_inMsg);
            
            MessageHelper.CopyMessageParts(pc, this.m_inMsg, message, messagePart);
            message.BodyPart.Data = stream;
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Assemble2:\tExiting");
            return message;
        }

        public void GetClassID(out Guid classId)
        {
            classId = new Guid("6469e38f-712d-4086-9795-78103ab670f4");
        }

        public void InitNew()
        {
        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp:Load:\tEntered");
            using (new DisposableObjectWrapper(new object[1]
      {
        (object) propertyBag
      }))
            {
                object obj1 = PropertyHelper.ReadPropertyBag(propertyBag, "HeaderSpecName");
                if (obj1 != null)
                {
                    this.m_pipelinecontextHeaderSpecName = (string)obj1;
                    this.m_headerSpecName = this.m_pipelinecontextHeaderSpecName;
                    this.headerSpecName = new SchemaWithNone(this.m_pipelinecontextHeaderSpecName);
                }
                object obj2 = PropertyHelper.ReadPropertyBag(propertyBag, "DocumentSpecName");
                if (obj2 != null)
                {
                    this.m_pipelinecontextDocumentSpecName = (string)obj2;
                    this.m_documentSpecName = this.m_pipelinecontextDocumentSpecName;
                    this.documentSpecName = new SchemaWithNone(this.m_pipelinecontextDocumentSpecName);
                }
                object obj3 = PropertyHelper.ReadPropertyBag(propertyBag, "TrailerSpecName");
                if (obj3 != null)
                {
                    this.m_pipelinecontextTrailerSpecName = (string)obj3;
                    this.m_trailerSpecName = this.m_pipelinecontextTrailerSpecName;
                    this.trailerSpecName = new SchemaWithNone(this.m_pipelinecontextTrailerSpecName);
                }
                object obj4 = PropertyHelper.ReadPropertyBag(propertyBag, "PreserveBom");
                if (obj4 != null)
                    this.m_preserveBom = (bool)obj4;
                object obj5 = PropertyHelper.ReadPropertyBag(propertyBag, "TargetCharset");
                object obj6 = PropertyHelper.ReadPropertyBag(propertyBag, "TargetCodePage");
                if (obj5 != null)
                {
                    if (obj6 != null)
                    {
                        if ((int)obj6 != 0)
                        {
                            this.strCharset = (string)obj5;
                            this.codepage = (int)obj6;
                            this.charset = new CharsetList(this.strCharset, this.codepage);
                            this.m_targetCharset = Encoding.GetEncoding(this.codepage).WebName;
                        }
                    }
                }
            }
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Load:\tExiting");
        }

        public void Save(IPropertyBag propertyBag, bool isClearDirty, bool isSaveAllProperties)
        {
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp:Save:\tEntered");
            using (new DisposableObjectWrapper(new object[1]
      {
        (object) propertyBag
      }))
            {
                object ptrVar1 = (object)this.m_pipelinecontextHeaderSpecName;
                propertyBag.Write("HeaderSpecName", ref ptrVar1);
                ptrVar1 = (object)this.m_pipelinecontextDocumentSpecName;
                propertyBag.Write("DocumentSpecName", ref ptrVar1);
                ptrVar1 = (object)this.m_pipelinecontextTrailerSpecName;
                propertyBag.Write("TrailerSpecName", ref ptrVar1);
                object ptrVar2 = (object)this.strCharset;
                propertyBag.Write("TargetCharset", ref ptrVar2);
                ptrVar2 = (object)this.codepage;
                propertyBag.Write("TargetCodePage", ref ptrVar2);
                ptrVar2 = (object)(bool)(this.m_preserveBom );
                propertyBag.Write("PreserveBom", ref ptrVar2);
                ptrVar2 = (object)"TargetCodePage";
                propertyBag.Write("HiddenProperties", ref ptrVar2);
            }
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Save:\tExiting");
        }

        private bool DoesThisSchemaExist(Schema usedSchema, object projectSystem)
        {
            bool flag = false;
            if (projectSystem != null)
            {
                foreach (Schema schema in SchemaRetriever.GetSchemas(projectSystem))
                {
                    if (schema.SchemaName == usedSchema.SchemaName)
                        return true;
                }
            }
            return flag;
        }

        public IEnumerator Validate(object projectSystem)
        {
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp:Validate:\tEntered");
            IEnumerator enumerator = (IEnumerator)null;
            ArrayList arrayList = new ArrayList();
            if (this.documentSpecName != null && this.documentSpecName.SchemaName.Length > 0 && !this.DoesThisSchemaExist((Schema)this.documentSpecName, projectSystem))
                arrayList.Add((object)string.Format((IFormatProvider)CultureInfo.CurrentCulture, FFAsmComp.resManager.GetString("ErrorDocSpecNotFound"), new object[1]
        {
          (object) this.documentSpecName.SchemaName
        }));
            if (this.headerSpecName != null && this.headerSpecName.SchemaName.Length > 0 && !this.DoesThisSchemaExist((Schema)this.headerSpecName, projectSystem))
                arrayList.Add((object)string.Format((IFormatProvider)CultureInfo.CurrentCulture, FFAsmComp.resManager.GetString("ErrorHeaderSpecNotFound"), new object[1]
        {
          (object) this.headerSpecName.SchemaName
        }));
            if (this.trailerSpecName != null && this.trailerSpecName.SchemaName.Length > 0 && !this.DoesThisSchemaExist((Schema)this.trailerSpecName, projectSystem))
                arrayList.Add((object)string.Format((IFormatProvider)CultureInfo.CurrentCulture, FFAsmComp.resManager.GetString("ErrorTrailerSpecNotFound"), new object[1]
        {
          (object) this.trailerSpecName.SchemaName
        }));
            if (arrayList.Count > 0)
                enumerator = arrayList.GetEnumerator();
            WPPTrace.WriteLine(WPPTrace.Debug, "FFAsmComp.Validate:\tExiting");
            return enumerator;
        }
    }
}
