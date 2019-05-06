using Vue24Hour.Domain.Model;

namespace Vue24Hour.Models
{
    public class AccountModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }

        internal static AccountModel MapFrom(Account account)
        {
            return new AccountModel
            {
                Name = account.Name,
                Phone = account.Phone
            };
        }
    }

    public class LoginModel
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
