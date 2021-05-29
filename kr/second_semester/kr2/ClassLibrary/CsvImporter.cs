using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class CsvImporter
    {
        public static void ImportFromCsv(string csvFilePath, GameRepository gRepo, PlatformRepository pRepo, RecordRepository rRepo)
        {
            CsvDataReader reader = new CsvDataReader(csvFilePath);

            while (reader.Read())
            {
                string platformName = reader.GetString(2);
                Platform platform = GetOrInsertPlatform(platformName, pRepo, reader);

                string gameName = reader.GetString(1);
                Game game = GetOrInsertGame(gameName, gRepo, reader);

                if (rRepo.GetByIds(game.id, platform.id) == null)
                {
                    Record release = new Record
                    {
                        gameId = game.id,
                        platformId = platform.id,
                        year = reader.GetInt32(3)
                    };

                    rRepo.InsertRecord(release);
                }
            }
            reader.Close();
        }
        private static Game GetOrInsertGame(string gameName, GameRepository repository, CsvDataReader reader)
        {
            Game game = repository.GetByName(gameName);
            if (game == null)
            {
                game = new Game();
                game.name = gameName;
                game.genre = reader.GetString(4);
                game.publisher = reader.GetString(5);

                game.id = repository.InsertGame(game);
            }

            return game;
        }
        private static Platform GetOrInsertPlatform(string platformName, PlatformRepository repository, CsvDataReader reader)
        {
            Platform platform = repository.GetByName(platformName);
            if (platform == null)
            {
                platform = new Platform();
                platform.name = reader.GetString(2);

                platform.id = repository.InsertPlatform(platform);
            }

            return platform;
        }
    }
}
