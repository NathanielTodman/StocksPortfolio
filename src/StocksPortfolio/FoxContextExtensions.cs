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
            var NewUsers = new List<Users>()
            {
                new Users()
                {
                    Username = "oded",
                    FirstName = "Sunantha",
                    Surname = "Thurajane",
                    Email = "Sunantha@gmail.com",
                    Password = "love",
                    Portfolio = new List<Transactions>()
                    {
                        new Transactions()
                        {
                            Symbol = "NFLX"
                        },
                        new Transactions()
                        {
                            Symbol = "RNET"
                        }

                    }
                },
                new Users()
                {
                    Username = "nat",
                    FirstName = "Nathaniel",
                    Surname = "Todman",
                    Email = "86nato@gmail.com",
                    Password = "love",
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
