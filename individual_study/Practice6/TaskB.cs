using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Practice6
{
    public static class TaskB
    {
        [XmlRoot("configuration")]
        public class Configuration
        {
            public string filePath;
            [XmlAttribute()]
            public bool skipIntro;
            public Adjustments adjustments;

            public override string ToString()
            {
                return $"FilePath: {filePath}, SkipIntro: {skipIntro}, Adjustments: [brightness: {adjustments.brightness}, contrast: {adjustments.contrast}]";
            }
        }
        public class Adjustments
        {
            [XmlAttribute()]
            public float brightness;
            [XmlAttribute()]
            public float contrast;
        }

        public static void Run()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Configuration));
            StreamReader sr = new StreamReader(@"D:\progbase\individual_study\Practice6\taskB.xml");
            Configuration configuration = (Configuration)ser.Deserialize(sr);
            sr.Close();
            Console.WriteLine(configuration);
        }
    }
}
