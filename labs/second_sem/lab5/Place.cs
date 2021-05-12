using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class Place
    {
        public string building;
        public string room;

        public override string ToString()
        {
            return $"{building}, room {room}";
        }
    }
}
