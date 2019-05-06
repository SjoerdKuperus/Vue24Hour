using System;

namespace Vue24Hour.Domain.Model
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}