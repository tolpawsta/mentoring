using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace BasicXml.Library.Servises
{
    public static class XmlParserService
    {
        public static string GetStringValue(this XElement element, string nameElement)
        {
            
            return element.Descendants().FirstOrDefault(e=>e.Name.LocalName.Equals(nameElement)).Value;
        }

        public static List<string> GetListSubElements(this XElement element, string nameSumElement)
        {
            return element.Descendants()?.Where(e => e.Name.LocalName.Equals(nameSumElement)).Select(e => e.Value).ToList();
        }
        public static DateTime GetDate(this XElement element, string nameElement)
        {
            
            return DateTime.Parse(element.GetStringValue(nameElement));
        }

        public static int GetInt(this XElement element, string nameElement)
        {
            return int.Parse(element.GetStringValue(nameElement));
        }
    }
}