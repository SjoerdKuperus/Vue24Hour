namespace Vue24Hour.Models
{
    public class AccountModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
    }

    public class LoginModel
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
