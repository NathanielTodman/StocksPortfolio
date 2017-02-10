using System.ComponentModel.DataAnnotations;

namespace StocksPortfolio.ViewModels
{
    public class TransactionModel
    {
        [Required(ErrorMessage = "Company symbol is required")]
        [StringLength(6)]
        public string Symbol { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}