using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ClassLibrary
{
    public static class XmlExporter
    {
        public static void ExportPlatforms(string filePath, PlatformRepository repo)
        {
            List<Platform> platforms = repo.GetAllPlatforms();
            XmlSerializer ser = new XmlSerializer(typeof(List<Platform>));
            System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath);
            ser.Serialize(writer, platforms);
            writer.Close();
        }
        public static void ExportGame(string filePath, string gameName, GameRepository gRepo, PlatformRepository pRepo)
        {
            Game game = gRepo.GetByName(gameName);
            game.platforms = pRepo.GetPlatformsByGame(game.id);

            XmlSerializer ser = new XmlSerializer(typeof(Game));
            System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath);
            ser.Serialize(writer, game);
            writer.Close();
        }
        public static void ExportPlatform(string filePath, string platformName, GameRepository gRepo, PlatformRepository pRepo)
        {
            Platform platform = pRepo.GetByName(platformName);
            platform.games = gRepo.GetGamesByPlatform(platform.id);
            XmlSerializer ser = new XmlSerializer(typeof(Platform));
            System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath);
            ser.Serialize(writer, platform);
            writer.Close();
        }
    }
}
