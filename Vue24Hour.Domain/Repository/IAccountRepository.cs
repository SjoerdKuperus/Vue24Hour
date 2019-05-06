using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Vue24Hour.Domain.Model;

namespace Vue24Hour.Domain.Repository
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts(params Expression<Func<Account, dynamic>>[] includeProperties);
        void CreateAccount(string phone, string password, string name);
        Account GetAccount(string phone, string password);
    }
}
