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
            // breakfast items
            new MenuItem {
				Id = 1001,
				Name = "Two eggs and toast",
				Price = (decimal)6.09,
				Description = "Two eggs sunny side up and wheat toast",
				Category = MenuItemCategory.Breakfast
			},
			new MenuItem {
				Id = 1002,
				Name = "Steak and Two eggs",
				Price = (decimal)10.09,
				Description = "Small steak and two eggs",
				Category = MenuItemCategory.Breakfast
			},



			new MenuItem {
				Id = 2000,
				Name = "Fried Chicken & Waffle",
				Price = (decimal)16.79,
				Description = "Enjoy a half portion of crispy fried chicken paired with a fluffy Belgian waffle, dusted with powdered sugar and accompanied by luscious strawberry butter. Complemented with our special sweet and spicy sauce, it's a true culinary delight.",
				Category = MenuItemCategory.Lunch
			},
			new MenuItem {
				Id = 2001,
				Name = "Steak & Scramble Bowl",
				Price = (decimal)13.79,
				Description = "Savor our seasoned sirloin steak alongside fluffy scrambled eggs and roasted grape tomatoes, all atop golden hashbrown potatoes and finished with a decadent drizzle of hollandaise sauce.",
				Category = MenuItemCategory.Lunch
			},
			new MenuItem {
				Id = 2002,
				Name = "Strawberry Shortcake Waffle",
				Price = (decimal)10.49,
				Description = "Delight in our distinctive Belgian waffle, adorned with a luscious drizzle of strawberry cream cheese icing, fresh strawberries, delectable cobbler crumble, and a sprinkle of powdered sugar.",
				Category = MenuItemCategory.Lunch
			},
			new MenuItem {
				Id = 2003,
				Name = "Country Fried Steak",
				Price = (decimal)16.99,
				Description = "Indulge in our tender country fried steak, smothered in rich homemade sausage gravy, accompanied by two eggs cooked to your preference and your choice of side.",
				Category = MenuItemCategory.Lunch
			},
			new MenuItem {
				Id = 2004,
				Name = "Tangy Chicken Bowl",
				Price = (decimal)10.99,
				Description = "Savor our tender, lightly fried chicken, enhanced with a zesty pineapple-orange sauce and garnished with fresh scallions. Presented on a bed of seasoned white rice alongside a colorful medley of shredded carrots, red cabbage, corn, green peppers, and onion.",
				Category = MenuItemCategory.Lunch
			},

            new MenuItem {
                Id = 2005,
                Name = "Hamburger",
                Price = (decimal)3.68,
                Description = "It's a cheese burger without the cheese",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 2006,
                Name = "Hamburger - double",
                Price = (decimal)5.70,
                Description = "It's a cheese burger without the cheese, with two beef patties",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 2007,
                Name = "Cheeseburger",
                Price = (decimal)4.09,
                Description = "A hamburger with cheese",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 2008,
                Name = "Cheeseburger - double",
                Price = (decimal)5.09,
                Description = "A hamburger with cheese, with two beef patties",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 2009,
                Name = "Mushroom & Swiss burger",
                Price = (decimal)4.59,
                Description = "Mushroom & Swiss burger",
                Category = MenuItemCategory.Lunch
            },
            new MenuItem {
                Id = 2010,
                Name = "Mushroom & Swiss burger - double",
                Price = (decimal)6.09,
                Description = "Mushroom & Swiss burger, with two beef patties",
                Category = MenuItemCategory.Lunch
            },


            // dinner items

			new MenuItem {
				Id = 3000,
				Name = "Steak and mashed potatoes",
				Price = (decimal)15.09,
				Description = "Ribeye Steak and mashed potatoes",
				Category = MenuItemCategory.Dinner
			},
			new MenuItem {
				Id = 3001,
				Name = "Fish and chips",
				Price = (decimal)14.49,
				Description = "Golden beer-battered white fish, fried to perfection, accompanied by tartar sauce, coleslaw, and seasoned fries.",
				Category = MenuItemCategory.Dinner
			},
			new MenuItem {
				Id = 3002,
				Name = "Sam's famous meatloaf",
				Price = (decimal)14.49,
				Description = "Sam's renowned creation: succulent griddle-seared meatloaf smothered in a luscious brown gravy, served alongside velvety mashed potatoes and tender steamed green beans.",
				Category = MenuItemCategory.Dinner
			},
            new MenuItem {
				Id = 3003,
				Name = "Sam's famous Chicken Pot Pie",
				Price = (decimal)14.49,
				Description = "Indulge in Sam's signature dish: tender chicken, carrots, celery, and corn bathed in our velvety cream sauce, all crowned with our unique homemade biscuit crust.",
				Category = MenuItemCategory.Dinner
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
    }
}
