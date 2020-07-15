using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    public interface IAccountRepository
    {
        Account GetAccount(int id);
        Account AddAccount(Account account);
        Account GetAccount(string username, string password);
        Account GetAccount(string username);

    }
}
