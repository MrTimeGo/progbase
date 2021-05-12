using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace lab5
{
    static class XmlDataIO
    {
        public static void StoreCoursesToFile(string filePath, List<Course> courses)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Root));
            StreamWriter writer = new StreamWriter(filePath);
            Root root = new Root();
            root.courses = courses;
            ser.Serialize(writer, root);
            writer.Close();
        }
        public static List<Course> GetCoursesFromFile(string filePath)
        {
            ValidateFile(filePath);

            XmlSerializer ser = new XmlSerializer(typeof(Root));
            StreamReader reader = new StreamReader(filePath);
            Root root;
            try
            {
                root = (Root)ser.Deserialize(reader);
            }
            catch
            {
                throw new Exception("Cannot deserialize file");
            }
            reader.Close();

            return root.courses;
        }
        private static void ValidateFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist");
            }
        }
    }
}
