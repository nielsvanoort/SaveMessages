using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.PipelineOM;
using Microsoft.Test.BizTalk.PipelineObjects;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Stage = Microsoft.Test.BizTalk.PipelineObjects.Stage;
using System.Collections.Specialized;
using SaveMessages.PipelinesExecution;


namespace SaveMessages.Pipeline
{
    public class PipeLineExecuter
    {


        // statics die in de gerefactorde  microsoft dll al gedeclareerd staan. Kunnen wat mij betreft in een volgende slag naar Constants toe
        #region Statics
        private static Hashtable stageDescriptors = (Hashtable)null;
        private static object syncRoot = new object();
        private static readonly Guid decodeStageId = new Guid("9d0e4103-4cce-4536-83fa-4a5040674ad6");
        private static readonly Guid disassembleStageId = new Guid("9d0e4105-4cce-4536-83fa-4a5040674ad6");
        private static readonly Guid validateStageId = new Guid("9d0e410d-4cce-4536-83fa-4a5040674ad6");
        private static readonly Guid resolvePartyStageId = new Guid("9d0e410e-4cce-4536-83fa-4a5040674ad6");
        private static readonly Guid preassembleStageId = new Guid("9d0e4101-4cce-4536-83fa-4a5040674ad6");
        private static readonly Guid assembleStageId = new Guid("9d0e4107-4cce-4536-83fa-4a5040674ad6");
        private static readonly Guid encodeStageId = new Guid("9d0e4108-4cce-4536-83fa-4a5040674ad6");
        private static readonly Guid SendCategoryId = new Guid("8c6b051c-0ff5-4fc2-9ae5-5016cb726282");
        private static readonly Guid ReceiveCategoryId = new Guid("f66b9f5e-43ff-4f5f-ba46-885348ae1b4e");
        private const string decodeStageName = "Decode";
        private const string disassembleStageName = "Disassemble";
        private const string validateStageName = "Validate";
        private const string resolvePartyStageName = "ResolveParty";
        private const string preassembleStageName = "Pre-Assemble";
        private const string assembleStageName = "Assemble";
        private const string encodeStageName = "Encode";
        private const ExecuteMethod decodeStageExecuteMethod = ExecuteMethod.All;
        private const ExecuteMethod disassembleStageExecuteMethod = ExecuteMethod.FirstMatch;
        private const ExecuteMethod validateStageExecuteMethod = ExecuteMethod.All;
        private const ExecuteMethod resolvePartyStageExecuteMethod = ExecuteMethod.All;
        private const ExecuteMethod preassembleStageExecuteMethod = ExecuteMethod.All;
        private const ExecuteMethod assembleStageExecuteMethod = ExecuteMethod.All;
        private const ExecuteMethod encodeStageExecuteMethod = ExecuteMethod.All;
        private const string XsiNamespaceUri = "http://www.w3.org/2001/XMLSchema-instance";
        public static NameValueCollection assemblies = new NameValueCollection();

        #endregion



        /// <summary>
        /// Voert de pipeline uit en geeft de resultaten in lijst terug
        /// </summary>
        /// <param name="pipelineType"> Het Tyoe van de Pipeline kan verkergen worden door typeof() </param>
        /// <param name="message"> uit te voeren message </param>
        /// <param name="path"> pad naar de dll van het Type</param>
        /// <param name="context"> Pipeline Context</param>
        /// <returns></returns>
        public static List<IBaseMessage> ExecutePipeline(Type pipelineType, IBaseMessage message, string path, PipelineContext context)
        {
            // voegt de schema's die zijn meegeleverd aan de pipeline toe aan de pipelinecontext zodat deze in de pipeline bendaderd kunnen worden.
            foreach (FileInfo fi in new DirectoryInfo(path).GetFiles("*.dll"))
            {
                Assembly asmAssembly =   Assembly.LoadFile(fi.FullName);
                foreach (var type in asmAssembly.GetTypes())
                {
                    assemblies.Add(type.Assembly.FullName, fi.FullName);
                    if (((asmAssembly.GetTypes()[0]).BaseType).FullName.ToLower().Contains("schemabase"))
                    {
                        try
                        {
                            Microsoft.BizTalk.Component.Interop.DocumentSpec docspec = new Microsoft.BizTalk.Component.Interop.DocumentSpec(type.FullName, type.Assembly.FullName);
                            context.AddDocSpecByName(type.AssemblyQualifiedName, docspec);
                        }
                        catch
                        {
                            // hier zou logging moeten komen
                        }
                    }


                }
            }




            var pipeline = CreatePipelineFromType(pipelineType, path);
            pipeline.InputMessages.Add(message);
            List<IBaseMessage> result = new List<IBaseMessage>();
            
            // kopier de standaard properties van de pipeline naar de messagecontext zodat ze daar ook bereikbaar zijn
            foreach (DictionaryEntry prop in ((SendPipeline) pipeline).CopyContext.Properties)
            {

                message.Context.Write(prop.Key.ToString().Split('@')[0], PipelineConstants.SaveMessageNamespace, prop.Value);
            }

            // voer de pipeline uit en zet de berichten in een lijst
            pipeline.Execute(context);
            IBaseMessage resultMessage = pipeline.GetNextOutputMessage(context);
            while (resultMessage != null)
            {
              result.Add(resultMessage);
              resultMessage = pipeline.GetNextOutputMessage(context);
            }

            return result;

        }

        /// <summary>
        /// Maakt een een pipeline aan vanuit een type en een dll pad
        /// </summary>
        /// <param name="pipelineType"> Het type pipeline dat gemaakt moet worden</param>
        /// <param name="path">pad naar de dll's om dynamisch in te kunnen laden</param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
        public static IPipeline CreatePipelineFromType(Type pipelineType, string path)
        {
            //Enabled hier geconfigureerd de Lagacy runtime die we nodig hebben voor het inladen dan de DLL's
            RuntimePolicyHelper.EnableLegacyV2Runtime();

            if (pipelineType == null)
                throw new ArgumentNullException("pipelineType");
            object instance = Activator.CreateInstance(pipelineType);
            object obj = pipelineType.InvokeMember("XmlContent", BindingFlags.GetProperty, (Binder)null, instance, (object[])null, CultureInfo.InvariantCulture);
            if (obj == null)
                throw new InvalidOperationException("XmlContent property of the pipeline object returned null");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml((string)obj);
            XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/Document/CategoryId");
            if (xmlNode1 == null)
                throw new InvalidOperationException("CategoryId node can not be found in the pipeline XML content");
            Guid categoryId = new Guid(xmlNode1.InnerText);
            IPipeline pipeline;
            if (categoryId == SendCategoryId)
            {
                pipeline = new SendPipeline(categoryId);
            }
            else
            {

                throw new Exception("Deze Liberary is alleen geschikt voor sendpipelines");

                // code laten staan voor wanneer dit gerefactord wordt en er een bus van gemaakt wordt.
                // dit betekend wel extra werk voor het mogelijk maken
                //if (!(categoryId == PipelineFactory.ReceiveCategoryId))
                //    throw new InvalidOperationException("Unknown pipeline category Id");
                //pipeline = (IPipeline)new ReceivePipeline(categoryId);
            }


            foreach (XmlNode Stage in xmlDocument.SelectNodes("/Document/Stages/Stage"))
            {

                // zet de standaard properties op de stage en voegt de stage toe

                XmlNode PolicyFileStage = Stage.SelectSingleNode("PolicyFileStage");
                if (PolicyFileStage == null)
                    throw new InvalidOperationException("PolicyFileStage element is missing");
                XmlNode PolicyFileStageName = (XmlNode)PolicyFileStage.Attributes["Name"]; 
                if (PolicyFileStageName == null)
                    throw new InvalidOperationException("Name attribute is missing");
                string PolicyFileStageNameText = PolicyFileStageName.InnerText;
                XmlNode execMethod = (XmlNode)PolicyFileStage.Attributes["execMethod"];
                if (execMethod == null)
                    throw new InvalidOperationException("execMethod attribute is missing");
                ExecuteMethod executeMethod = (ExecuteMethod)Enum.Parse(typeof(ExecuteMethod), execMethod.InnerText, true);
                XmlNode stageId = (XmlNode)PolicyFileStage.Attributes["stageId"];
                if (stageId == null)
                    throw new InvalidOperationException("stageId attribute is missing");
                Guid id = new Guid(stageId.InnerText);
                Stage stage = new Stage(PolicyFileStageNameText, executeMethod, id, pipeline);
                pipeline.Stages.Add((object)stage);

                // zet de componenten op de Stage

                foreach (XmlNode componentNode in Stage.SelectNodes("Components/Component"))
                {
                    IBaseComponent pipelineComponent = PipeLineExecuter.CreatePipelineComponent(componentNode, path, (SendPipeline)pipeline);
                    stage.AddComponent(pipelineComponent);
                }
            }
            return pipeline;
        }


        /// <summary>
        /// Laad dynamisch de pipeline componenten
        /// </summary>
        /// <param name="componentNode"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static IBaseComponent CreatePipelineComponent(XmlNode componentNode , string path, SendPipeline pipeline)
        {
            XmlNode xmlNode1 = componentNode.SelectSingleNode("Name");
            if (xmlNode1 == null)
                throw new InvalidOperationException("Component name node is not found");
            IBaseComponent pipelineComponent = PipeLineExecuter.CreatePipelineComponent(xmlNode1.InnerText, path);
            IPropertyBag propertyBag = (IPropertyBag)new BTMPropertyBag();
            foreach (XmlNode xmlNode2 in componentNode.SelectNodes("Properties/Property"))
            {
                if (xmlNode2.Attributes["Name"] == null)
                    throw new InvalidOperationException("Name attribute is not found for a property node");
                string innerText = xmlNode2.Attributes["Name"].InnerText;
                XmlNode valueNode = xmlNode2.SelectSingleNode("Value");
                if (valueNode != null)
                {
                    object typedPropertyValue = PipeLineExecuter.GetTypedPropertyValue(valueNode);
                    propertyBag.Write(innerText, ref typedPropertyValue);
                    pipeline.CopyContext.Write(innerText, PipelineConstants.SaveMessageNamespace,typedPropertyValue);
                }
            }
            ((IPersistPropertyBag)pipelineComponent).Load(propertyBag, 0);
            return pipelineComponent;
        }


        [SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
        public static IBaseComponent CreatePipelineComponent(string typeName, string path)
        {

            // flat file wordt nog gereroute wegens dat deze de foutmeldingen wrapped en daardoor kun je niet meer zien wat er echt fout gaat.

            if (typeName.Equals("Microsoft.BizTalk.Component.FFAsmComp,Microsoft.BizTalk.Pipeline.Components, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"))
                return new FFAsmComp();
            Type type = Type.GetType(typeName);
            if (type != null)
            {
                IBaseComponent baseComponent = (IBaseComponent)Activator.CreateInstance(type);
                if (baseComponent != null)
                    return baseComponent;
            }

            // we willen de assemblies openen in het aangeleverde pad en niet in het BizTalk installatie pad.
           // string path = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\BizTalk Server\\3.0").GetValue("InstallPath").ToString() + "Pipeline Components";
           
            //strip de rotzooi van de fulltypename af
            if (typeName.IndexOf(',') >= 0)
                typeName = typeName.Substring(0, typeName.IndexOf(','));


            //probeert vanuit alle aangeleverde dll's het type te maken wanneer aanwezig wordt dit gereturnd.
            foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles("*.dll"))
            {
                try
                {
                    IBaseComponent baseComponent = (IBaseComponent)Assembly.LoadFrom(fileInfo.FullName).CreateInstance(typeName);
                    if (baseComponent != null)
                        return baseComponent;
                }
                catch
                {
                }
            }
            throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Can't instantiate pipeline component component with type name {0}.", new object[1]
      {
        (object) typeName
      }));
        }

        #region gegenereerdeCode

        /// 
        ///  alles hieronder is gegenereerde code 
        /// 
        /// 
        /// 




        private static object GetTypedPropertyValue(XmlNode valueNode)
        {
            if (valueNode == null)
                throw new ArgumentNullException("valueNode");
            XmlNamespaceManager nsmgr = new XmlNamespaceManager((XmlNameTable)new NameTable());
            nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            string str = valueNode.SelectSingleNode("@xsi:type", nsmgr).InnerText;
            string[] strArray = str.Split(':');
            if (strArray.Length > 1)
                str = strArray[1];
            string innerText = valueNode.InnerText;
            if ("int" == str)
                return (object)Convert.ToInt32(innerText, (IFormatProvider)CultureInfo.InvariantCulture);
            if ("boolean" == str)
                return (object)(bool)(Convert.ToBoolean(innerText, (IFormatProvider)CultureInfo.InvariantCulture));
            if ("char" == str)
                return (object)Convert.ToChar((object)int.Parse(innerText, (IFormatProvider)CultureInfo.InvariantCulture), (IFormatProvider)CultureInfo.InvariantCulture);
            else
                return (object)innerText;
        }







        private static IBaseMessageFactory _factory = new MessageFactory();

        /// <summary>
        /// Creates a new message with the specified string
        /// as the body part.
        /// </summary>
        /// <param name="body">Content of the body</param>
        /// <returns>A new message</returns>
        public static IBaseMessage CreateFromString(string body)
        {
            if (body == null)
                throw new ArgumentNullException("body");

            byte[] content = Encoding.Unicode.GetBytes(body);
            Stream stream = new MemoryStream(content);

            IBaseMessage msg = CreateFromStream(stream);
            msg.BodyPart.Charset = "UTF-16";
            return msg;
        }


        /// <summary>
        /// Create a new message with the specified stream as 
        /// the body part.
        /// </summary>
        /// <param name="body">Body of the message</param>
        /// <returns>A new message object</returns>
        public static IBaseMessage CreateFromStream(Stream body)
        {
            if (body == null)
                throw new ArgumentNullException("body");

            IBaseMessage message = _factory.CreateMessage();
            message.Context = _factory.CreateMessageContext();
            IBaseMessagePart bodyPart = CreatePartFromStream(body);

            message.AddPart("body", bodyPart, true);

            return message;
        }

        /// <summary>
        /// Creates a new message part with the specified data
        /// </summary>
        /// <param name="body">Data of the part</param>
        /// <returns>The new part</returns>
        public static IBaseMessagePart CreatePartFromString(string body)
        {
            if (body == null)
                throw new ArgumentNullException("body");

            byte[] content = Encoding.Unicode.GetBytes(body);
            Stream stream = new MemoryStream(content);

            IBaseMessagePart part = CreatePartFromStream(stream);
            part.Charset = "UTF-16";
            return part;
        }

        /// <summary>
        /// Creates a new message part
        /// </summary>
        /// <param name="body">Body of the part</param>
        /// <returns>The new part</returns>
        public static IBaseMessagePart CreatePartFromStream(Stream body)
        {
            if (body == null)
                throw new ArgumentNullException("body");

            IBaseMessagePart part = _factory.CreateMessagePart();
            part.Data = body;
            return part;
        }

        /// <summary>
        /// Creates a multi-part message from an array
        /// of strings. The first string in the array will be marked
        /// as the message body part
        /// </summary>
        /// <param name="parts">One string for each part</param>
        /// <returns>The new message</returns>
        public static IBaseMessage Create(params String[] parts)
        {
            if (parts == null || parts.Length < 1)
                throw new ArgumentException("Need to specify at least one part", "parts");

            IBaseMessage message = CreateFromString(parts[0]);
            for (int i = 1; i < parts.Length; i++)
                message.AddPart("part" + i, CreatePartFromString(parts[i]), false);
            return message;
        }

        /// <summary>
        /// Creates a multi-part message from an array
        /// of streams. The first stream in the array will be marked
        /// as the message body part
        /// </summary>
        /// <param name="parts">One stream for each part</param>
        /// <returns>The new message</returns>
        public static IBaseMessage Create(params Stream[] parts)
        {
            if (parts == null || parts.Length < 1)
                throw new ArgumentException("Need to specify at least one part", "parts");

            IBaseMessage message = CreateFromStream(parts[0]);
            for (int i = 1; i < parts.Length; i++)
                message.AddPart("part" + i, CreatePartFromStream(parts[i]), false);
            return message;
        }

        /// <summary>
        /// Helper method to consume a stream
        /// </summary>
        /// <param name="stream">Stream to consume</param>
        public static void ConsumeStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            byte[] buffer = new byte[4096];
            int read = 0;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                ;
        }
        /// <summary>
        /// Helper method to consume the message body part stream
        /// </summary>
        /// <param name="message">Message to consume</param>
        public static void ConsumeStream(IBaseMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            ConsumeStream(message.BodyPart);
        }
        /// <summary>
        /// Helper method to consume the part stream
        /// </summary>
        /// <param name="part">Part to consume</param>
        public static void ConsumeStream(IBaseMessagePart part)
        {
            if (part == null)
                throw new ArgumentNullException("part");
            ConsumeStream(part.Data);
        }
        /// <summary>
        /// Helper method to read back a stream as a string
        /// </summary>
        /// <param name="stream">Stream to consume</param>
        /// <param name="encoding">Expected encoding of the stream contents</param>
        public static string ReadString(Stream stream, Encoding encoding)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            using (StreamReader reader = new StreamReader(stream, encoding))
                return reader.ReadToEnd();
        }
        /// <summary>
        /// Helper method to read back a stream as a string
        /// </summary>
        /// <param name="message">Message to consume</param>
        public static string ReadString(IBaseMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            return ReadString(message.BodyPart);
        }
        /// <summary>
        /// Helper method to read back a stream as a string
        /// </summary>
        /// <param name="part">Part to consume</param>
        public static String ReadString(IBaseMessagePart part)
        {
            if (part == null)
                throw new ArgumentNullException("part");
            Encoding enc = Encoding.UTF8;
            if (!String.IsNullOrEmpty(part.Charset))
                enc = Encoding.GetEncoding(part.Charset);
            return ReadString(part.Data, enc);
        }

        /// <summary>
        /// Loads a BizTalk message from the set of files exported from
        /// the BizTalk Admin Console or HAT
        /// </summary>
        /// <param name="contextFile">Path to the *_context.xml file</param>
        /// <returns>The loaded message</returns>
        /// <remarks>
        /// Context files have no type information for properties
        /// in the message context, so all properties are 
        /// added as strings to the context.
        /// </remarks>
        public static IBaseMessage LoadMessage(string contextFile)
        {
            IBaseMessage msg = _factory.CreateMessage();
            IBaseMessageContext ctxt = _factory.CreateMessageContext();
            msg.Context = ctxt;

            XPathDocument doc = new XPathDocument(contextFile);
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator props = nav.Select("//Property");
            foreach (XPathNavigator prop in props)
            {
                ctxt.Write(
                   prop.GetAttribute("Name", ""),
                   prop.GetAttribute("Namespace", ""),
                   prop.GetAttribute("Value", "")
                   );
            }

            XPathNodeIterator parts = nav.Select("//MessagePart");
            foreach (XPathNavigator part in parts)
            {
                LoadPart(msg, part, contextFile);
            }
            return msg;
        }

        private static void LoadPart(IBaseMessage msg, XPathNavigator node, string contextFile)
        {
            // don't care about the id because we can't set it anyway
            string name = node.GetAttribute("Name", "");
            string filename = node.GetAttribute("FileName", "");
            string charset = node.GetAttribute("Charset", "");
            string contentType = node.GetAttribute("ContentType", "");
            bool isBody = XmlConvert.ToBoolean(node.GetAttribute("IsBodyPart", ""));

            XmlResolver resolver = new XmlUrlResolver();
            Uri realfile = resolver.ResolveUri(new Uri(contextFile), filename);
            IBaseMessagePart part = CreatePartFromStream(File.OpenRead(realfile.LocalPath));
            part.Charset = charset;
            part.ContentType = contentType;
            msg.AddPart(name, part, isBody);
        }
    }

    #endregion 
}
