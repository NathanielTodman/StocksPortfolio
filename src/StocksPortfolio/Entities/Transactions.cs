using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Entities
{
    public class Transactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Company { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool Buy { get; set; }
    }
}
