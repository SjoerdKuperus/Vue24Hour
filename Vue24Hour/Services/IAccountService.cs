using System.Collections.Generic;
using System.Threading.Tasks;
using Vue24Hour.Models;

namespace Vue24Hour.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountModel>> GetItems();
        Task AddAccount(string phone, string password);
    }
}
