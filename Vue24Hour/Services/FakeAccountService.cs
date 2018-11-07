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
                new AccountModel {Phone = "06-11111111", Name = "Admin"}
            };
            Accounts = accounts.ToList();
        }

        public Task<IEnumerable<AccountModel>> GetItems()
        {
            return Task.FromResult(Accounts.AsEnumerable());
        }

        public Task AddAccount(string phone, string password, string name)
        {
            Accounts.Add(new AccountModel
            {
                Name = name,
                Phone = phone,
                Password = password,
                Password2 = password
            });
            return Task.CompletedTask;
        }

        public Task<AccountModel> GetAccount(string phone, string password)
        {
            var result = Accounts.SingleOrDefault(a => a.Phone == phone && a.Password == password);
            return Task.FromResult(result);
        }
    }
}
