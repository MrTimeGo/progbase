using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Task2
{
    static class DataIO
    {
        public static void WriteToCsv(string fout, List<Record> records)
        {
            StreamWriter sw = new StreamWriter(fout);

            foreach (Record record in records)
            {
                sw.WriteLine($"{record.year},{record.ceremony},{record.award},{record.winner},{record.name},{record.film}");
            }
            sw.Close();
        }
        public static List<Record> ReadFromXml(string f)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Root));
            StreamReader sr = new StreamReader(f);
            Root root = (Root)ser.Deserialize(sr);
            sr.Close();
            return root.records;
        }
    }
}
