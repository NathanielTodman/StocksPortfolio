using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Models
{
    public class CreateTransactionDTO
    {
        public string Symbol { get; set; }
        public string Company { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool Buy { get; set; }
    }
}
