using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Utils.Xml
{
    public static class XmlUtils
    {
        public static String ObjectToXml(Object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());

                serializer.Serialize(ms, obj);

                String namespaces = " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"";

                return Encoding.UTF8.GetString(ms.ToArray()).Replace(namespaces, ""); 
            }
        }

        public static T XmlToObject<T>(String xml)
        {
            using (StringReader reader = new StringReader(xml))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
            }
        }
    }
}
