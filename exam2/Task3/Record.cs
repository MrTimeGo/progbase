using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Task3
{
    [XmlRoot("root")]
    public class Record
    {
        [XmlElement("Year")]
        public int year;
        [XmlElement("Ceremony")]
        public int ceremony;
        [XmlElement("Award")]
        public string award;
        [XmlElement("Winner")]
        public string winner;
        [XmlElement("Name")]
        public string name;
        [XmlElement("Film")]
        public string film;

    }
}
