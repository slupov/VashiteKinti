using System;
using Xunit;
using VashiteKinti.Data.Models;
using VashiteKinti.Services;
using FluentAssertions;
using System.Linq;
using VashiteKinti.Data.Enums;

namespace VashiteKinti.Tests
{
    public class ServicesTests
    {

        [Fact]

        public void AddMethodShouldAddItems()
        {
            var db = VashiteKinti.Tests.Tests.GetDatabase();

            var item1 = new Bank()
            {
                Name = "ProCreditBank",
            };

            var item2 = new Bank()
            {
                Name = "DSK",
            };

            var items = new GenericDataService<Bank>(db);

            items.Add(item1, item2);

            var result = db.Banks.ToArray().Count();

            Assert.Equal(2, result);

        }


        [Fact]
        
        public void ShouldUpdateItems()
        {
            var db = VashiteKinti.Tests.Tests.GetDatabase();

            var item1 = new Bank()
            {
                Name = "ProCreditBank",

            };

            var item2 = new Bank()
            {
                Name = "DSK",
            };

            var items = new GenericDataService<Bank>(db);

            items.Add(item1, item2);


            item1.Name = "Updated";

            items.Update(item1);

            var editItem = db.Banks.FirstOrDefault(x => x.Name == item1.Name);

            Assert.Equal(item1.Name, editItem.Name);

        }



        [Fact]

        public void ShouldRemoveItem()
        
        {
            var db = VashiteKinti.Tests.Tests.GetDatabase();

            var item1 = new Bank()
            {
                Name="ProCreditBank"
            };





            var items = new GenericDataService<Bank>(db);

            db.Banks.Add(item1);
            db.SaveChanges();

            items.Remove(item1);

            var editItem = db.Banks.FirstOrDefault();

            Assert.Null(editItem);

        }

        [Fact]

        public void ShouldGetAll()
        {
            var db = VashiteKinti.Tests.Tests.GetDatabase();

            var item1 = new Bank()
            {
                Name = "ProCreditBank",

            };

            var item2 = new Bank()
            {
                Name = "DSK",
            };

            var items = new GenericDataService<Bank>(db);

            db.Banks.Add(item1);
            db.SaveChanges();

            var result = items.GetAllAsync();

            Assert.NotNull(result);

        }


        [Fact]

        public void ShouldCheckIfThereIsAnyBank()
        {
            var db = VashiteKinti.Tests.Tests.GetDatabase();

            var item1 = new Bank()
            {
                Name = "ProCreditBank",

            };

            var item2 = new Bank()
            {
                Name = "DSK",
            };

            var items = new GenericDataService<Bank>(db);

            db.Banks.Add(item1);
            db.SaveChanges();

            var result = items.AnyAsync();

            result.Result.Should().BeTrue();

        }


        [Fact]

        public void ShouldGetSingleProperty()
        {
            var db = VashiteKinti.Tests.Tests.GetDatabase();

            var item1 = new Bank()
            {
                Name = "ProCreditBank",

            };

            var item2 = new Bank()
            {
                Name = "DSK",
            };

            var items = new GenericDataService<Bank>(db);

            db.Banks.Add(item1);
            db.Banks.Add(item2);
            db.SaveChanges();

            var result = items.GetSingleOrDefaultAsync(e => e.Name == "DSK");

            result.Result.Name.Should().Be("DSK");

        }

        [Fact]

        public void ShouldGetDefaultProperty()
        {
            var db = VashiteKinti.Tests.Tests.GetDatabase();

            var item1 = new Bank()
            {
                Name = "ProCreditBank",

            };

            var item2 = new Bank()
            {
                Name = "DSK",
            };

            var items = new GenericDataService<Bank>(db);

            db.Banks.Add(item1);
            db.Banks.Add(item2);
            db.SaveChanges();

            var result = items.GetSingleOrDefaultAsync(x => x.Name == "DSK1");

            result.Result.Should().Be(null);

        }


        [Fact]

        public void ShouldSearchDeposit()
        {
            var db = VashiteKinti.Tests.Tests.GetDatabase();

            var item = new Bank()
            {
                Name = "ProCredit"
            };

            var item1 = new Deposit()
            {
                Name = "ProCreditDeposit",
                CreditOpportunity = YesNoDoesntMatter.YES,
                Bank = item,
                Currency = Currency.BGN,
                ExtraMoneyPayIn = YesNoDoesntMatter.DOESNT_MATTER,
                InterestType = InterestType.FIXED,
                Size = 2,
                Period = 6,
                Holder = DepositHolder.INDIVIDUAL,
                OverdraftOpportunity = YesNoDoesntMatter.YES,

            };

            var items = new GenericDataService<Bank>(db);
            db.Deposits.Add(item1);
            db.Banks.Add(item);
            var result = items.SearchDepositsByCriterias(2, "BGN", "6", "10", "INDIVIDUAL", "FIXED", "DOESNT_MATTER", "YES", "YES");

            Assert.NotNull(result.Result);

        }

    }

}
