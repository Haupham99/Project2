using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Services
{
    public class SQLAccountRepository : IAccountRepository
    {

        private AppDbContext _context;

        public SQLAccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public Account AddAccount(Account account)
        {
            _context.Account.Add(account);
            _context.SaveChanges();
            return account;
        }

        public Account GetAccount(int id)
        {
            return _context.Account.Find(id);
        }
        public Account GetAccount(string username, string password)
        {
            return _context.Account.Where(a => (a.username == username)).Where(a => (a.password == password)).FirstOrDefault();
        }
        public Account GetAccount(string username)
        {
            return _context.Account.Where(a => (a.username == username)).FirstOrDefault();
        }
    }
}
