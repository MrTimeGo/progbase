using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ClassLibrary
{
    [XmlRoot("game")]
    public class Game
    {
        public int id;
        public string name;
        public string genre;
        public string publisher;

        public List<Platform> platforms;
    }
}
