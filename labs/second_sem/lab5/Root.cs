using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace lab5
{
    [XmlType("root")]
    public class Root
    {
        [XmlElement("course")]
        public List<Course> courses;
    }
}
