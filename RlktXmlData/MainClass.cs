using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RlktXmlData
{
    public class Object2Xml
    {
        public static string Serialize(object cls)
        {
            if (cls == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(cls.GetType());
                var settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter,settings))
                {
                    xmlserializer.Serialize(writer, cls);
                    return stringWriter.ToString();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
    }

    public class Xml2Object
    {
        public static object DeSerialize(string fileName, object inputClass)
        {
            if (inputClass == null)
            {
                return null;
            }
            try
            {

                //XmlRootAttribute rootAttribute = new XmlRootAttribute("S");
                XmlSerializer xmlserializer = new XmlSerializer(inputClass.GetType());
                XmlReader readData = XmlReader.Create(fileName);
                return xmlserializer.Deserialize(readData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("An error occurred", ex);
            }
        }
    }
}
