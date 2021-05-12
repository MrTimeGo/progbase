using System;
using System.Xml.Serialization;

namespace lab5
{
    public class Time
    {
        [XmlElement("start_time")]
        public string startTime;
        [XmlElement("end_time")]
        public string endTime;

        public override string ToString()
        {
            return $"{startTime} till {endTime}";
        }
    }
}
