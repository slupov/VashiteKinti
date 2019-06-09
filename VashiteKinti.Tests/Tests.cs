using Microsoft.EntityFrameworkCore;
using System;
using VashiteKinti.Data;

namespace VashiteKinti.Tests
{
    public class Tests
    {
        

        public static VashiteKintiDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<VashiteKintiDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new VashiteKintiDbContext(dbOptions);
        }
    }
}