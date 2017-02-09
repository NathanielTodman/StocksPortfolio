using System.ComponentModel.DataAnnotations;

namespace StocksPortfolio.ViewModels
{
    public class TransactionModel
    {
        [Required]
        public string Symbol { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}