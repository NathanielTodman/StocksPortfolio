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
        IEnumerable<Users> GetUsers();
        Users GetUser(int userId, bool includePortfolio);
        IEnumerable<Transactions> GetPortfolio(int userId);
        Transactions GetTransaction(int userId, int transactionId);
    }
}
