using System;
using System.Diagnostics;

namespace SimpleClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTests();
            Console.WriteLine("All tests are passed");
            Ticket ticket1 = new Ticket();
            Ticket ticket2 = new Ticket("Scorpions", 1299, new DateTime(2021, 8, 5, 19, 30, 0), "Palace of sport", "Petselia Artem");
            Ticket ticket3 = new Ticket("Queen", double.MaxValue, new DateTime(1986, 6, 10, 14, 0, 0), "Wembley stadium", "John Bon Jovi");
            Console.WriteLine("Ticket 2:\n{0}", ticket2.ToString());
            Console.WriteLine("Ticket 3:\n{0}", ticket3.ToString());
        }
        static void RunTests()
        {
            DateTime currentDate = DateTime.Now;
            DateTime date1 = new DateTime(2021, 4, 2, 3, 45, 23);
            Ticket testTicket = new Ticket(date1);
            Debug.Assert((int)testTicket.GetRemainingTime().TotalDays == (int)(date1 - currentDate).TotalDays);
            Debug.Assert(testTicket.GetRemainingTime().Hours == (date1 - currentDate).Hours);
            Debug.Assert(testTicket.GetRemainingTime().Minutes == (date1 - currentDate).Minutes);
            Debug.Assert(testTicket.GetRemainingTime().Seconds == (date1 - currentDate).Seconds);
        }
    }
    class Ticket
    {
        public string band;
        public double price;
        public DateTime beginningTime;
        public string place;
        public string owner;

        public Ticket()
        {

        }
        public Ticket(string band, double price, DateTime beginningTime, string place, string owner)
        {
            this.band = band;
            this.price = price;
            this.beginningTime = beginningTime;
            this.place = place;
            this.owner = owner;
        }
        public Ticket(DateTime beginningTime)
        {
            this.beginningTime = beginningTime;
        }
        public override string ToString()
        {
            return string.Format("Band: {0}\nTime: {1}\nPlace: {2}\nPrice: {3}\nOwner: {4}",
                band,
                beginningTime.ToString(),
                place,
                price,
                owner); 
        }
        public TimeSpan GetRemainingTime()
        {
            return beginningTime - DateTime.Now;
        }
    }
}
