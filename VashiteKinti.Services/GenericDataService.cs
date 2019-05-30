using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VashiteKinti.Data;
using VashiteKinti.Data.Enums;
using VashiteKinti.Data.Models;

namespace VashiteKinti.Services
{
    public class GenericDataService<T> : IGenericDataService<T> where T : class
    {
        protected DbSet<T> _dbSet;
        protected VashiteKintiDbContext context;

        public GenericDataService(VashiteKintiDbContext dbContext)
        {
            this._dbSet = dbContext.Set<T>();
            this.context = dbContext;
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync<T>();
        }
        public Task<List<Deposit>> GetFilteredDeposits(string currencyId, string interestId)
        {
            var currency = (Currency)Enum.Parse(typeof(Currency), currencyId);
            var interest = (InterestPaymentMethod)Enum.Parse(typeof(InterestPaymentMethod), interestId);
            var data = context.Deposits.Where(x => x.Currency == currency && x.PaymentMethod == interest).ToList();
            return Task.FromResult(data);
        }

        public Task<List<Deposit>> SearchDepositsByCriterias(int depositSize, string currency, string depositPeriod,
            string interest, string depositHolder, string interestType, string extraMoneyPayIn,
            string overdraftOpportunity, string creditOpportunity)
            {
            var currencyValue = (Currency)Enum.Parse(typeof(Currency), currency);
            var interestValue = (InterestPaymentMethod)Enum.Parse(typeof(InterestPaymentMethod), interest);
            var depositPeriodValue = int.Parse(depositPeriod);
            var depositHolderValue = (DepositHolder)Enum.Parse(typeof(DepositHolder), depositHolder);
            var extraMoneyPayInValue = (YesNoDoesntMatter)Enum.Parse(typeof(YesNoDoesntMatter), extraMoneyPayIn);
            var overdraftOpportunityValue = (YesNoDoesntMatter)Enum.Parse(typeof(YesNoDoesntMatter), overdraftOpportunity);
            var creditOpportunityValue = (YesNoDoesntMatter)Enum.Parse(typeof(YesNoDoesntMatter), creditOpportunity);
            var interestTypeValue = (InterestType)Enum.Parse(typeof(InterestType), interestType);

            var filter = context.Deposits.Where(x => x.Currency == currencyValue);
            filter = filter.Where(x => x.PaymentMethod == interestValue);
            filter = filter.Where(x => x.MinAmount <= depositSize);

            if (depositHolderValue != DepositHolder.DOESNT_MATTER)
            {
                filter = filter.Where(x => x.Holder == depositHolderValue);

            }
            if (extraMoneyPayInValue != YesNoDoesntMatter.DOESNT_MATTER)
            {
                filter = filter.Where(x => x.ExtraMoneyPayIn == extraMoneyPayInValue);

            }
            if (overdraftOpportunityValue != YesNoDoesntMatter.DOESNT_MATTER)
            {
                filter = filter.Where(x => x.OverdraftOpportunity == overdraftOpportunityValue);

            }
            if (creditOpportunityValue != YesNoDoesntMatter.DOESNT_MATTER)
            {
                filter = filter.Where(x => x.CreditOpportunity == creditOpportunityValue);

            }
            if (interestTypeValue != InterestType.DOESNT_MATTER)
            {
                filter = filter.Where(x => x.InterestType == interestTypeValue);

            }


            return Task.FromResult(filter.ToList());
            }

        public virtual Task<List<T>> GetListAsync(Func<T, bool> where)
        {
            return Task.Run(() => _dbSet.AsEnumerable().Where(where).ToList());
        }

        public virtual Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return _dbSet.SingleOrDefaultAsync(where);
        }

        public T GetSingleOrDefault(Expression<Func<T, bool>> where)
        {
            return _dbSet.SingleOrDefault(where);
        }

        public virtual void Add(params T[] items)
        {
            foreach (T item in items)
            {
                context.Add(item);
            }

            context.SaveChanges();
        }

        public virtual void Update(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public virtual void Remove(params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Deleted;
            }

            context.SaveChanges();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return _dbSet.AnyAsync(where);
        }

        public Task<bool> AnyAsync()
        {
            return _dbSet.AnyAsync();
        }
    }
}
