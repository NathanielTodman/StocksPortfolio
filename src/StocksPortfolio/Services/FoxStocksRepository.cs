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
using AutoMapper;
using StocksPortfolio.Models;

namespace StocksPortfolio.Services
{
    public class FoxStocksRepository : IFoxStocksRepository
    {
        private FoxContext _context;

        public FoxStocksRepository(FoxContext context)
        {
            _context = context;
        }

        //function to lookup company from passed in transaction model
        public async Task<Transactions> Lookup(Transactions transaction)
        {
            string url = ("http://download.finance.yahoo.com/d/quotes.csv?s=" + transaction.Symbol + "&f=snl1");
            var request = WebRequest.Create(url);
            request.Method = "GET";
            var response = await request.GetResponseAsync();
            var reader = new CsvReader(new StreamReader(response.GetResponseStream()));
            string[] results = reader.Parser.Read();
            transaction.Symbol = results[0];
            transaction.Company = results[1];
            if (results[2] != "N/A")
            {
                transaction.Price = Convert.ToDouble(results[2]);
            }
            return transaction;
        }

        //This method to check company by their symbol
        public async Task<Transactions> LookupPrice(string Symbol)
        {
            string url = ("http://download.finance.yahoo.com/d/quotes.csv?s=" + Symbol + "&f=snl1");
            var request = WebRequest.Create(url);
            request.Method = "GET";
            var response = await request.GetResponseAsync();
            var reader = new CsvReader(new StreamReader(response.GetResponseStream()));
            string[] results = reader.Parser.Read();
            var transaction = new Transactions();
            transaction.Symbol = results[0].ToUpper();
            transaction.Company = results[1];
            transaction.Price = Convert.ToDouble(results[2]);
            return transaction;
        }

        //get current user
        public FoxUser GetUser(string userId)
        {
            return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }

        //get users portfolio, return array
        public IEnumerable<Portfolio> GetPortfolio(string userId)
        {
            return _context.Portfolio.Where(c => c.FoxUserId == userId).ToList();            
        }

        // add transaction to database, inc server side validation
        public void AddTransaction(string userId, Transactions transaction)
        {
            //total cost of shares being purchased/sold
            var totalPurchase = transaction.Price * transaction.Quantity;
            //get current user
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            //get portfolio of current user for this particular stock
            var portfolio = _context.Portfolio.Where(
                p => p.FoxUserId == userId && p.Symbol == transaction.Symbol).FirstOrDefault();
            var transactionDTO = Mapper.Map<TransactionDTO>(transaction);
            if (transaction.Quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0");
            }
            if (transaction.Buy == true && user.Cash < totalPurchase)
            {
                Console.WriteLine("User does not have enough cash to make this purchase");
            }
            // if it passes all validation, and transaction is buy:
            else if(transaction.Buy == true)
            {
                user.Cash -= totalPurchase;
                _context.Transactions.Add(transaction);
                if(portfolio == null)
                {
                    var newPortfolio = Mapper.Map<Portfolio>(transactionDTO);                    
                    newPortfolio.Total = totalPurchase;
                    newPortfolio.FoxUserId = userId;
                    _context.Portfolio.Add(newPortfolio);
                }
                else
                {
                    portfolio.Quantity += transaction.Quantity;
                    portfolio.Total += totalPurchase;
                    portfolio.LastPrice = transaction.Price;
                }
            }
            //else if sell:
            else if(transaction.Buy == false)
            {
                if(portfolio == null)
                {
                    Console.WriteLine("User does not own any of this stock to sell");
                }
                else if(portfolio.Quantity - transaction.Quantity < 0)
                {
                    Console.WriteLine("User does not have enough of this stock to sell");
                }
                else if(portfolio.Quantity - transaction.Quantity == 0)
                {
                    _context.Portfolio.Remove(portfolio);                   
                    user.Cash += totalPurchase;
                    _context.Transactions.Add(transaction);
                }
                else
                {
                    portfolio.Quantity -= transaction.Quantity;
                    portfolio.Total -= totalPurchase;
                    portfolio.LastPrice = transaction.Price;
                    user.Cash += totalPurchase;
                    _context.Transactions.Add(transaction);
                }
            }
        }

        //delete a transaction from database (admin function, not implemented for users)
        public void DeleteTransaction(Transactions transaction)
        {
            _context.Transactions.Remove(transaction);
        }
        
        //function to retrieve transaction history for user
        public IEnumerable<Transactions> GetTransactions(string userId)
        {
            return _context.Transactions.Where(c => c.FoxUserId == userId).ToList();
        }

        //return true if save is succesful, else throw exception
        public bool Save()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
