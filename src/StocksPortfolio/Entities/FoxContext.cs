using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StocksPortfolio.Entities
{
    public class FoxContext : IdentityDbContext<FoxUser>
    {
        public FoxContext(DbContextOptions<FoxContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
    }



}
