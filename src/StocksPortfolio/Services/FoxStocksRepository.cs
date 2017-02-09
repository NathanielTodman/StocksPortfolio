using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StocksPortfolio.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO;
using CsvHelper;
using Microsoft.AspNetCore.Identity;

namespace StocksPortfolio.Services
{
    public class FoxStocksRepository : IFoxStocksRepository
    {
        private FoxContext _context;

        public FoxStocksRepository(FoxContext context)
        {
            _context = context;
        }

        public async Task<Transactions> Lookup(string Symbol)
        {
            string url = ("http://download.finance.yahoo.com/d/quotes.csv?s=" + Symbol + "&f=snl1");
            var request = WebRequest.Create(url);
            request.Method = "GET";
            var response = await request.GetResponseAsync();
            var reader = new CsvReader(new StreamReader(response.GetResponseStream()));
            string[] results = reader.Parser.Read();

            var transaction = new Transactions();
            transaction.Symbol = results[0];
            transaction.Company = results[1];
            transaction.Price = Convert.ToDouble(results[2]);
            return transaction;
        }

        public FoxUser GetUser(string userId)
        {
            return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }

        public IEnumerable<Transactions> GetPortfolio(string userId)
        {
            return _context.Transactions.Where(c => c.FoxUserId == userId).ToList();            
        }

        public void AddTransaction(string userId, Transactions transaction)
        {
            var totalPurchase = transaction.Price * transaction.Quantity;
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (transaction.Buy == true && user.Cash < totalPurchase)
            {
                throw new Exception("User does not have enough cash to make this purchase");
            }
            else if(transaction.Buy == true)
            {
                user.Cash -= totalPurchase;
                _context.Transactions.Add(transaction);
            }
            else if(transaction.Buy == false)
            {
                user.Cash += totalPurchase;
                _context.Transactions.Add(transaction);
            }
        }

        public void DeleteTransaction(Transactions transaction)
        {
            _context.Transactions.Remove(transaction);
        }
        
        public Transactions GetTransaction(string userId, int transactionId)
        {
            return _context.Transactions.Where(c => c.FoxUserId == userId && c.Id == transactionId).FirstOrDefault();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
