using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    static class DataProcessor
    {
        public static int CountCeremonies(List<Record> records)
        {
            int currentCeremony = 0;
            int counter = 0;
            foreach (Record record in records)
            {
                if (record.ceremony != currentCeremony)
                {
                    counter++;
                    currentCeremony = record.ceremony;
                }
            }
            return counter;
        }
        public static List<Record> GetRecordByCeremony(int ceremony, List<Record> allCeremonies)
        {
            List<Record> list = new List<Record>();
            for (int i = 0; i < allCeremonies.Count; i++)
            {
                if (allCeremonies[i].ceremony == ceremony)
                {
                    list.Add(allCeremonies[i]);
                }
            }
            return list;
        }
    }
}
