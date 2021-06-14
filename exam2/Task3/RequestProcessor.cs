using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Task3
{
    static class RequestProcessor
    {
        public static string GetResponse(string[] subcommands, Repository repo)
        {
            switch (subcommands[1])
            {
                case "ceremonies": return ProcessCeremonies(subcommands, repo);
                case "films": return ProcessFilms(subcommands, repo);
                default: throw new ArgumentException("Unknown path");
            }
        }
        private static string ProcessCeremonies(string[] subcommands, Repository repo)
        {
            if (!int.TryParse(subcommands[2], out int year))
            {
                throw new ArgumentException("Year should be number");
            }
            if (subcommands[3] != "nominations")
            {
                throw new ArgumentException("Unknown path");
            }
            string award = subcommands[4];
            award = award.Replace("%20", " ");
            if (subcommands[5] != "winner")
            {
                throw new ArgumentException("Unknown path");
            }
            string winnerName = repo.GetWinnerByYearAndNomination(year, award);

            return Serialize(winnerName);
        }
        private static string ProcessFilms(string[] subcommands, Repository repo)
        {
            string film = subcommands[2];
            film = film.Replace("%20", " ");
            if (subcommands[3] != "nominations")
            {
                throw new ArgumentException("Unknown path");
            }

            List<Record> records = repo.GetNominationsByFilm(film);
            return Serialize(records);
        }
        private static string Serialize<T>(T value)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            StringWriter sw = new StringWriter();
            ser.Serialize(sw, value);
            sw.Close();
            return sw.ToString();
        }
    }
}
