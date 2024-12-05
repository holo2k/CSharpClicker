﻿using CSharpClicker.Web.Domain;
using CSharpClicker.Web.Infrastructure.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Initializers
{
    public class DbContextInitializer
    {
        public static void AddAppDbContext(IServiceCollection services)
        {
            var pathToDbFile = GetPathToDbFile();
            services
                .AddDbContext<AppDbContext>(options => options
                    .UseSqlite($"Data Source={pathToDbFile}"));

            string GetPathToDbFile()
            {
                var applicationFolder = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "CSharpClicker");

                if (!Directory.Exists(applicationFolder))
                {
                    Directory.CreateDirectory(applicationFolder);
                }

                return Path.Combine(applicationFolder, "CSharpClicker.db");
            }
        }

        public static void InitializeDbContext(AppDbContext dbContext)
        {
            
            const string Boost1 = "Пехотинец";
            const string Boost2 = "Лучник";
            const string Boost3 = "Ассасин";
            const string Boost4 = "Маг света";
            const string Boost5 = "Крестьянин с копьём";

           dbContext.Database.Migrate();

            var existingBoosts = dbContext.Boosts.ToArray();

            AddBoostIfNotExist(Boost1, price: 100, profit: 1);
            AddBoostIfNotExist(Boost2, price: 500, profit: 15);
            AddBoostIfNotExist(Boost3, price: 2000, profit: 60, isAuto: true);
            AddBoostIfNotExist(Boost4, price: 10000, profit: 400);
            AddBoostIfNotExist(Boost5, price: 100000, profit: 5000, isAuto: true);

            AddRandomUsers();

            dbContext.SaveChanges();

            void AddRandomUsers()
            {
                return;

                const int limit = 130000534;
                const int asciLimit = 126;
                const int symbolsLimit = 15;
                const int symbolsStart = 5;

                var random = new Random();

                for (var i = 0; i < 200; i++)
                {
                    var score = random.Next(limit);
                    var symbolsCount = random.Next(symbolsStart, symbolsLimit);

                    var userName = string.Empty;
                    for (var j = 0; j < symbolsCount; j++)
                    {
                        var character = random.Next(asciLimit);
                        userName += char.ConvertFromUtf32(character);
                    }

                    dbContext.Users.Add(new ApplicationUser
                    {
                        UserName = userName,
                        RecordScore = score,
                    });
                }
            }

            void AddBoostIfNotExist(string name, long price, long profit, bool isAuto = false)
            {
                if (!existingBoosts.Any(eb => eb.Title == name))
                {
                    var pathToImage = Path.Combine(".","Resources", "BoostImages",$"{name}.jpg");
                    using var fileStream = File.OpenRead(pathToImage);
                    using var memoryStream = new MemoryStream();

                    fileStream.CopyTo(memoryStream);

                    dbContext.Boosts.Add(new Boost
                    {
                        Title = name,
                        Price = price,
                        Profit = profit,
                        IsAuto = isAuto,
                        Image = memoryStream.ToArray()
                    });
                }
            }
        }
    }
}
