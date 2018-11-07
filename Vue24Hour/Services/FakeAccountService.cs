using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue24Hour.Models;

namespace Vue24Hour.Services
{
    public class FakeAccountService : IAccountService
    {
        public List<AccountModel> Accounts { get; set; }

        public FakeAccountService()
        {
            var accounts = new[]
            {
                new AccountModel {Phone = "0612345678"}
            };
            Accounts = accounts.ToList();
        }

        public Task<IEnumerable<AccountModel>> GetItems()
        {
            return Task.FromResult(Accounts.AsEnumerable());
        }

        public Task AddAccount(string phone, string password)
        {
            Accounts.Add(new AccountModel {Phone = phone, Password = password, Password2 = password});
            return Task.CompletedTask;
        }
    }
}
