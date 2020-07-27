using System;
using System.Xml;

namespace BasicXml.Libruary.Servises
{
    public static class XmlParserService
    {
        public static string GetStringValue(this XmlReader reader, string nameElement)
        {
            reader.ReadToFollowing(nameElement);
            return reader.ReadElementContentAsString();
        }

        public static DateTime GetDate(this XmlReader reader, string nameElement)
        {
            reader.ReadToFollowing(nameElement);
            return reader.ReadElementContentAsDateTime();
        }

        public static uint GetUint(this XmlReader reader, string nameElement)
        {
            reader.ReadToFollowing(nameElement);
            uint itemValue = 1;
            try
            {
                uint.TryParse(reader.ReadElementContentAsString(), out itemValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
            }
            return itemValue;
        }
    }
}