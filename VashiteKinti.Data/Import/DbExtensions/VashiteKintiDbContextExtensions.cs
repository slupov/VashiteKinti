using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VashiteKinti.Data.Models;

namespace VashiteKinti.Data.Import.DbExtensions
{
    public static class VashiteKintiDbContextExtensions
    {
        public static void EnsureSeedData(this VashiteKintiDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any()) return;

            var dataImportPath  = Directory.GetCurrentDirectory();

            if (dataImportPath.EndsWith("bin\\Debug"))
            {
                dataImportPath = dataImportPath.Substring(0, dataImportPath.IndexOf("\\bin\\Debug"));
            }

            dataImportPath += "\\..\\VashiteKinti.Data\\Import\\";

            //products to list
            string json = File.ReadAllText(
                Path.GetFullPath(dataImportPath + "BanksImport.json"));
            var banksToSeed = JsonConvert.DeserializeObject<Bank[]>(json);
            SeedBanks(context, banksToSeed);

            json = File.ReadAllText(Path.GetFullPath(dataImportPath + "DepositsImport.json"));
            var depositsToSeed = JsonConvert.DeserializeObject<Deposit[]>(json);
            SeedDeposits(context, depositsToSeed);

            json = File.ReadAllText(Path.GetFullPath(dataImportPath + "CardsImport.json"));
            var cardsToSeed = JsonConvert.DeserializeObject<Card[]>(json);
            SeedCards(context, cardsToSeed);

            json = File.ReadAllText(Path.GetFullPath(dataImportPath + "CreditsImport.json"));
            var creditsToSeed = JsonConvert.DeserializeObject<Credit[]>(json);
            SeedCredits(context, creditsToSeed);

            json = File.ReadAllText(Path.GetFullPath(dataImportPath + "InsurancesImport.json"));
            var insurancesToSeed = JsonConvert.DeserializeObject<Insurance[]>(json);
            SeedInsurances(context, insurancesToSeed);

            json = File.ReadAllText(Path.GetFullPath(dataImportPath + "InvestmentsImport.json"));
            var investmentsToSeed = JsonConvert.DeserializeObject<Investment[]>(json);
            SeedInvestements(context, investmentsToSeed);
        }

        private static void SeedInvestements(VashiteKintiDbContext context, Investment[] investmentsToSeed)
        {
            if (context.Investments.Any()) return;

            context.Investments.AddRange(investmentsToSeed);
            context.SaveChanges();
        }

        private static void SeedInsurances(VashiteKintiDbContext context, Insurance[] insurancesToSeed)
        {
            if (context.Insurances.Any()) return;

            context.Insurances.AddRange(insurancesToSeed);
            context.SaveChanges();
        }

        private static void SeedCredits(VashiteKintiDbContext context, Credit[] creditsToSeed)
        {
            if (context.Credits.Any()) return;

            context.Credits.AddRange(creditsToSeed);
            context.SaveChanges();
        }

        private static void SeedCards(VashiteKintiDbContext context, Card[] cardsToSeed)
        {
            if (context.Cards.Any()) return;

            context.Cards.AddRange(cardsToSeed);
            context.SaveChanges();
        }

        private static void SeedBanks(VashiteKintiDbContext context, Bank[] banksToSeed)
        {
            if (context.Banks.Any()) return;

            context.Banks.AddRange(banksToSeed);
            context.SaveChanges();
        }

        private static void SeedDeposits(VashiteKintiDbContext context, Deposit[] depositsToSeed)
        {
            if (context.Deposits.Any()) return;

            context.Deposits.AddRange(depositsToSeed);
            context.SaveChanges();
        }
    }
}
