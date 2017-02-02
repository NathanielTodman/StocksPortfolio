using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StocksPortfolio.Entities
{
    public class FoxContext : IdentityDbContext<FoxUser>
    {
        public FoxContext(DbContextOptions<FoxContext> options) : base(options)
        {
            Database.Migrate();
        }

        
        public DbSet<Transactions> Transactions { get; set; }
    }



}
