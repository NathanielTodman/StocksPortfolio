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

        public IEnumerable<Transactions> GetPortfolio(int userId)
        {
            return _context.Transactions.Where(c => c.UserId == userId).ToList();
        }

        public Transactions GetTransaction(int userId, int transactionId)
        {
            return _context.Transactions.Where(c => c.UserId == userId && c.Id == transactionId).FirstOrDefault();
        }

        public Users GetUser(int userId, bool includePortfolio)
        {
            if (includePortfolio)
            {
                return _context.Users.Include(p => p.Portfolio).Where(c => c.Id == userId).FirstOrDefault();
            }
            return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }

        public IEnumerable<Users> GetUsers()
        {
            return _context.Users.OrderBy(c=>c.Surname).ToList();
        }
    }
}
