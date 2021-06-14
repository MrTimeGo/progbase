using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Task1
{
    class DataIO
    {
        public static int[] ReadFromFile(string f)
        {
            List<int> list = new List<int>();
            StreamReader sr = new StreamReader(f);
            while (true)
            {
                string str = sr.ReadLine();
                if (str == null)
                {
                    break;
                }
                if (int.TryParse(str, out int num))
                {
                    list.Add(num);
                }
            }
            sr.Close();
            int[] array = new int[list.Count];
            list.CopyTo(array);
            return array;
        }
        public static void WriteToFile(string f, int[] array)
        {
            StreamWriter sw = new StreamWriter(f);
            for (int i = 0; i < array.Length; i++)
            {
                sw.WriteLine(array[i]);
            }
            sw.Close();
        }
        public static Vector[] ReadFromXml(string f)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Vector[]));
            StreamReader sr = new StreamReader(f);
            Vector[] array = (Vector[])ser.Deserialize(sr);
            sr.Close();
            return array;
        }
        public static void WriteToXml(string fout, Vector[] array)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Vector[]));
            StreamWriter sw = new StreamWriter(fout);
            ser.Serialize(sw, array);
            sw.Close();
        }
    }
}
