using System;
using System.Xml.Serialization;
using System.IO;

namespace Practice6
{
    public static class TaskA
    {
        public class Configuration
        {
            public string filePath;
            public bool skipIntro;
            public Adjustments adjustments;

            public override string ToString()
            {
                return $"FilePath: {filePath}, SkipIntro: {skipIntro}, Adjustments: [brightness: {adjustments.brightness}, contrast: {adjustments.contrast}]";
            }
        }
        public class Adjustments
        {
            public float brightness;
            public float contrast;
        }
        public static void Run()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Configuration));
            StreamReader sr = new StreamReader(@"D:\progbase\individual_study\Practice6\taskA.xml");
            Configuration configuration = (Configuration)ser.Deserialize(sr);
            sr.Close();
            Console.WriteLine(configuration);
        }
    }
}
