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
        public double Cash { get; set; }

        public ICollection<Transactions> Portfolio { get; set; }
        = new List<Transactions>();
    }
}