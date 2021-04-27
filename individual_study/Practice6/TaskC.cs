using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Practice6
{
    public static class TaskC
    {
        [XmlRoot("library")]
        public class Library
        {
            [XmlAttribute()]
            public string name;
            [XmlElement("book")]
            public List<Book> books;
        }
        public class Book
        {
            public string title;
            public string authors;
            [XmlAttribute()]
            public float price;
            public override string ToString()
            {
                return $"{title} - by {authors}. Price: {price}";
            }
        }
        public static void Run()
        {
            Library lib = GetLibrary();
            Console.WriteLine($"Library: {lib.name}");
            Book mostExpenciveBook = new Book();
            mostExpenciveBook.price = float.MinValue;
            float sum = 0;
            foreach (Book book in lib.books)
            {
                Console.WriteLine(book);
                if (book.price > mostExpenciveBook.price)
                {
                    mostExpenciveBook = book;
                }
                sum += book.price;
            }
            Console.WriteLine();
            Console.WriteLine($"Most expencive book: {mostExpenciveBook}");
            Console.WriteLine();
            Console.WriteLine($"Sum of prices: {sum}");
        }
        private static Library GetLibrary()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Library));
            StreamReader sr = new StreamReader(@"D:\progbase\individual_study\Practice6\taskC.xml");
            Library lib = (Library)ser.Deserialize(sr);
            sr.Close();
            return lib;
        }
    }
}
