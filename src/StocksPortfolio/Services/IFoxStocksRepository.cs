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
        IEnumerable<FoxUser> GetUsers();
        FoxUser GetUser(string userId);
        IEnumerable<Transactions> GetPortfolio(string userId);
        Transactions GetTransaction(string userId, int transactionId);
        void AddTransaction(string userId, Transactions transaction);
        void DeleteTransaction(Transactions transaction);
        bool Save();
    }
}
