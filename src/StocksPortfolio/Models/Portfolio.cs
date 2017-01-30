using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Models
{
    public class Portfolio
    {
        public string Symbol { get; set; }
        public string Company { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double CurrentPrice { get; set; }
        public double Total { get { return Total; } set { Total = Price * Quantity; } }
    }
}
