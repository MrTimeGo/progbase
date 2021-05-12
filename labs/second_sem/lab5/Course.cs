using System;
using System.Xml.Serialization;

namespace lab5
{
    public class Course
    {
        [XmlElement("reg_num")]
        public int registrationNumber;
        [XmlElement("subj")]
        public string subject;
        [XmlElement("crse")]
        public int courceId;
        [XmlElement("sect")]
        public string section;
        public string title;
        public float units;
        public string instructor;
        public string days;
        public Time time;
        public Place place;

        public override string ToString()
        {
            return $"[{registrationNumber}]  {courceId}) {title}. Subject: {subject}. - {section} - Units: {units}. Instructor: {instructor}. At {days} - {time} - {place}";
        }
    }
}
