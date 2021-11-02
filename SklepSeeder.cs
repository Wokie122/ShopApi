using ShopApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi
{
    public class SklepSeeder
    {
        private readonly SklepDbContext _dbContext;

        public SklepSeeder(SklepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Artykuly.Any())
                {
                    var artykuly = GetArtykuly();
                    _dbContext.Artykuly.AddRange(artykuly);
                    _dbContext.SaveChanges();
                }
            }
        }


        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };

            return roles;
        }

        private IEnumerable<Artykul> GetArtykuly()
        {
            var artykuly = new List<Artykul>()
            {
                new Artykul()
                {
                    Name = "Coca-Cola",
                    Description = "Napoj gazowany o smaku coli",
                    Price = 3.50M
                },
                new Artykul()
                {
                    Name = "Ziemniaki",
                    Description = "Warzywo",
                    Price = 6.50M
                }
            };
            return artykuly;
        }


    }
}
