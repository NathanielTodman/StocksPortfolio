using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StocksPortfolio.Entities
{
    public class Portfolio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Company { get; set; }
        public string FoxUserId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double LastPrice { get; set; }
        public double Change { get; set; }
        public double Total { get; set; }
    }
}
