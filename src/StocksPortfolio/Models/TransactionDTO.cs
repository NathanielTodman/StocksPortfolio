﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Models
{
    public class TransactionDTO
    {
        public bool Buy { get; set; }
        public string Symbol { get; set; }
        public string Company { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
