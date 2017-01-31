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
        bool UserExists(int userId);
        IEnumerable<Users> GetUsers();
        Users GetUser(int userId);
        IEnumerable<Transactions> GetPortfolio(int userId);
        Transactions GetTransaction(int userId, int transactionId);
        void AddTransaction(int userId, Transactions transaction);
        void DeleteTransaction(Transactions transaction);
        bool Save();
    }
}
