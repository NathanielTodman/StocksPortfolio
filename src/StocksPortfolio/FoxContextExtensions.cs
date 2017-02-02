using StocksPortfolio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio
{
    public static class FoxContextExtensions
    {
        public static void EnsureSeedDataForContext(this FoxContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            var NewUsers = new List<FoxUser>()
            {
                new FoxUser()
                {
                    UserName = "Oded",
                    PasswordHash = "P@ssw0rd!",
                    Email = "Sunantha@gmail.com",
                    Portfolio = new List<Transactions>()
                    {
                        new Transactions()
                        {
                            Symbol = "NFLX",
                            Company = "Netflix"
                        },
                        new Transactions()
                        {
                            Symbol = "RNET"
                        }

                    }
                },
                new FoxUser()
                {
                    UserName = "Nat",
                    PasswordHash = "P@ssw0rd!",
                    Email = "86nato@gmail.com",
                    Portfolio = new List<Transactions>()
                    {
                        new Transactions()
                        {
                            Symbol = "AVGO"
                        },
                        new Transactions()
                        {
                            Symbol = "MUTD"
                        }

                    }
                }
            };

            context.Users.AddRange(NewUsers);
            context.SaveChanges();
        }
    }
}
