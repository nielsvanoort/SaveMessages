using Microsoft.XLANGs.BaseTypes;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace SaveMessages.MapsExecution
{
    /// <summary>
    /// Class that executes maps 
    /// </summary>
    public class Mapper
    {


        public TransformBase Transform;
        /// <summary>
        /// performs a tramsfom from a XML to o
        /// </summary>
        /// <param name="inputXmlStream">stream containing the XML to be mapped</param>
        /// <returns></returns>
        /// 
        public Stream PerformTransform(Stream inputXmlStream)
        {
            Stream outputXmlFile = new MemoryStream();
            var xslCompiledTransform = new XslCompiledTransform();
            var xmlReaderstylesheet = XmlReader.Create(new StringReader(Transform.XmlContent));
            var xsltSetting = new XsltSettings(true, true);
            var xmlReaderInput = XmlReader.Create(inputXmlStream);


            xmlReaderInput.MoveToContent();
            var xmlWriterSetting = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t"
            };
            var xmlWriter = XmlWriter.Create(outputXmlFile, xmlWriterSetting);
            xslCompiledTransform.Load(xmlReaderstylesheet, xsltSetting, null);
            xslCompiledTransform.Transform(xmlReaderInput, xmlWriter);
            xmlWriter.Close();
            xmlReaderInput.Close();

            return outputXmlFile;
        }

    }
}

