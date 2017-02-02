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

        public void AddTransaction(string userId, Transactions transaction)
        {
            throw new NotImplementedException();
        }

        public void DeleteTransaction(Transactions transaction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transactions> GetPortfolio(string userId)
        {
            throw new NotImplementedException();
        }

        public Transactions GetTransaction(string userId, int transactionId)
        {
            throw new NotImplementedException();
        }

        public FoxUser GetUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoxUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Transactions> GetPortfolio(string userId)
        //{
        //    return _context.Transactions.Where(c => c.UserId == userId).ToList();
        //}

        //public Transactions GetTransaction(string userId, int transactionId)
        //{
        //    return _context.Transactions.Where(c => c.UserId == userId && c.Id == transactionId).FirstOrDefault();
        //}

        //public FoxUser GetUser(string userId)
        //{ 
        //    return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        //}

        //public IEnumerable<FoxUser> GetUsers()
        //{
        //    return _context.Users.OrderBy(c=>c.Surname).ToList();
        //}

        //public void AddTransaction(string userId, Transactions transaction)
        //{
        //    var user = GetUser(userId);
        //    user.Portfolio.Add(transaction);
        //}

        //public void DeleteTransaction(Transactions transaction)
        //{
        //    _context.Transactions.Remove(transaction);
        //}

        //public bool Save()
        //{
        //    return (_context.SaveChanges() >= 0);
        //}

    }
}
