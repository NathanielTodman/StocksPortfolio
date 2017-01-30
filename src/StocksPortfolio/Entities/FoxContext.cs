using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StocksPortfolio.Entities
{
    public class FoxContext : DbContext
    {
        public FoxContext(DbContextOptions<FoxContext> options) : base(options)
        {
            Database.Migrate();
        }


        public DbSet<Users> Users { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
    }



}
