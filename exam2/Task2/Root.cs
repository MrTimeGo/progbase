using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Task2
{
    [XmlRoot("root")]
    public class Root
    {
        [XmlElement("row")]
        public List<Record> records;
    }
}
