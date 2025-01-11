using System;
using System.Collections.Generic;

namespace FinanceManagementSystem
{
    public class Client
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Account> Accounts { get; private set; }

        public Client(string name, string email)
        {
            Name = name;
            Email = email;
            Accounts = new List<Account>();
        }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }
    }
}
