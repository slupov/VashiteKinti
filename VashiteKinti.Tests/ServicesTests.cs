using System;
using Xunit;
using VashiteKinti.Data.Models;
using VashiteKinti.Services;
using FluentAssertions;
using System.Linq;

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

    }
}
