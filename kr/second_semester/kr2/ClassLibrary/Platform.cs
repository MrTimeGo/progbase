using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ClassLibrary
{
    [XmlRoot("platform")]
    public class Platform
    {
        public int id;
        public string name;

        public List<Game> games;
    }
}
