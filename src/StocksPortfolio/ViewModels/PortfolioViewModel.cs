using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.ViewModels
{
    public class PortfolioViewModel
    {
        public IEnumerable<StocksPortfolio.Models.PortfolioDTO> Portfolio { get; set; }
        public IEnumerable<StocksPortfolio.ViewModels.TransactionModel> Transaction { get; set; }
    }
}
