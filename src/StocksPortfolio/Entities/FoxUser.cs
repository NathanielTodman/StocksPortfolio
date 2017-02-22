using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StocksPortfolio.Entities
{
    public class FoxUser : IdentityUser
    {
        //default starting cash
        public double Cash { get; set; } = 10000;
        public ICollection<Portfolio> Portfolio { get; set; }
        = new List<Portfolio>();
        public ICollection<Transactions> Transaction { get; set; }
        = new List<Transactions>();
    }
}