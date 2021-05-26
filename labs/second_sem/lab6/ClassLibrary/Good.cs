using System;

namespace ClassLibrary
{
    public class Good
    {
        public long id;
        public string name;
        public string description;
        public double price;
        public bool isAvailable;
        public DateTime createdAt;

        public override string ToString()
        {
            string available = isAvailable ? "Available" : "Not available";
            return $"[{id}] {name} - {price} UAH - {available}";
        }
    }
}
