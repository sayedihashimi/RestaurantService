using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyRestaurantApi;

namespace MyRestaurantApi.Data
{
    public class MyRestaurantApiContext : DbContext
    {
        public MyRestaurantApiContext (DbContextOptions<MyRestaurantApiContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MenuItem>().HasData(GetSeedDataMenuItems());
            modelBuilder.Entity<Contact>().HasData(GetSeedDataContacts());
        }
        public DbSet<MyRestaurantApi.Contact> Contact { get; set; } = default!;

        public DbSet<MyRestaurantApi.MenuItem> MenuItem { get; set; } = default!;

        public DbSet<MyRestaurantApi.MenuItemOrdered> MenuItemOrdered { get; set; } = default!;

        private MenuItem[] GetSeedDataMenuItems() => new MenuItem[] {
            new MenuItem {
                Id = 1,
                Name = "Hamburger",
                Price = (decimal)3.68,
                Description = "It's a cheese burger without the cheese",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 2,
                Name = "Hamburger - double",
                Price = (decimal)5.70,
                Description = "It's a cheese burger without the cheese, with two beef patties",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 3,
                Name = "Cheeseburger",
                Price = (decimal)4.09,
                Description = "A hamburger with cheese",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 4,
                Name = "Cheeseburger - double",
                Price = (decimal)5.09,
                Description = "A hamburger with cheese, with two beef patties",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 5,
                Name = "Mushroom & Swiss burger",
                Price = (decimal)4.59,
                Description = "Mushroom & Swiss burger",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 6,
                Name = "Mushroom & Swiss burger - double",
                Price = (decimal)6.09,
                Description = "Mushroom & Swiss burger, with two beef patties",
                Category = MenuItemCategory.Lunch
            }
        };
        private Contact[] GetSeedDataContacts() => new Contact[] {
            new Contact {
                Id = 1,
                Name = "Sayed Hashimi",
                Email = "sayed@example.com",
                Phone = "555-111-2222"
            },
            new Contact {
                Id=2,
                Name = "Mads Kristensen",
                Email = "mads@example.com",
                Phone = "555-111-3333"
            },
            new Contact {
                Id=3,
                Name = "Eline Barstad",
                Email = "elineb@example.com",
                Phone = "555-111-4444"
            },
            new Contact {
                Id=4,
                Name = "Theodore Lamy",
                Email = "theol@example.com",
                Phone = "555-111-5555"
            },
            new Contact {
                Id=5,
                Name = "María Zelaya",
                Email = "mariaz@example.com",
                Phone = "555-111-6666"
            },
            new Contact {
                Id=6,
                Name = "Kubanychbek Sagynbek",
                Email = "kubans@example.com",
                Phone = "555-111-7777"
            },
            new Contact {
                Id=7,
                Name = "Denise Bourgeois",
                Email = "deniseb@example.com",
                Phone = "555-111-8888"
            },
            new Contact {
                Id=8,
                Name = "Robin Danielsen",
                Email = "robind@example.com",
                Phone = "555-111-9999"
            }
        };
        public DbSet<MyRestaurantApi.TogoOrder> TogoOrder { get; set; } = default!;
        public DbSet<MyRestaurantApi.AdminContact> AdminContact { get; set; } = default!;
    }
}
