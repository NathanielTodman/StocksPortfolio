using StocksPortfolio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Services
{
    public interface IFoxStocksRepository
    {
        Task<Transactions> Lookup(string symbol);
        FoxUser GetUser(string userId);
        IEnumerable<Portfolio> GetPortfolio(string userId);
        IEnumerable<Transactions> GetTransactions(string userId);
        void AddTransaction(string userId, Transactions transaction);
        void DeleteTransaction(Transactions transaction);
        bool Save();
    }
}
