using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue24Hour.Domain.Repository;
using Vue24Hour.Models;

namespace Vue24Hour.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<IEnumerable<AccountModel>> GetItems()
        {
            var allAccounts = _accountRepository.GetAllAccounts();
            return Task.FromResult(allAccounts.Select(_ => AccountModel.MapFrom(_)).AsEnumerable());
        }

        public Task AddAccount(string phone, string password, string name)
        {
            _accountRepository.CreateAccount(phone, password, name);
            return Task.CompletedTask;
        }

        public Task<AccountModel> GetAccount(string phone, string password)
        {
            var result = AccountModel.MapFrom(_accountRepository.GetAccount(phone, password));
            return Task.FromResult(result);
        }
    }
}
