using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StocksPortfolio.Entities;
using Microsoft.EntityFrameworkCore;

namespace StocksPortfolio.Services
{
    public class FoxStocksRepository : IFoxStocksRepository
    {
        private FoxContext _context;

        public FoxStocksRepository(FoxContext context)
        {
            _context = context;
        }

        public bool UserExists(int userId)
        {
            var check = _context.Users.Where(u=> u.Id == userId).ToList();
            if(check == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Transactions> GetPortfolio(int userId)
        {
            return _context.Transactions.Where(c => c.UserId == userId).ToList();
        }

        public Transactions GetTransaction(int userId, int transactionId)
        {
            return _context.Transactions.Where(c => c.UserId == userId && c.Id == transactionId).FirstOrDefault();
        }

        public Users GetUser(int userId)
        { 
            return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }

        public IEnumerable<Users> GetUsers()
        {
            return _context.Users.OrderBy(c=>c.Surname).ToList();
        }

        public void AddTransaction(int userId, Transactions transaction)
        {
            var user = GetUser(userId);
            user.Portfolio.Add(transaction);
        }

        public void DeleteTransaction(Transactions transaction)
        {
            _context.Transactions.Remove(transaction);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
