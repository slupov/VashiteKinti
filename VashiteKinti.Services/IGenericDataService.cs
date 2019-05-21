using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VashiteKinti.Data.Models;

namespace VashiteKinti.Services
{
    public interface IGenericDataService<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<List<T>> GetListAsync(Func<T, bool> where);

        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> where);
        Task<List<Deposit>> GetFilteredDeposits(string currencyId, string interestId);

        T GetSingleOrDefault(Expression<Func<T, bool>> where);

        void Add(params T[] items);

        void Update(params T[] items);

        void Remove(params T[] items);

        Task<bool> AnyAsync(Expression<Func<T, bool>> where);

        Task<bool> AnyAsync();
    }
}
