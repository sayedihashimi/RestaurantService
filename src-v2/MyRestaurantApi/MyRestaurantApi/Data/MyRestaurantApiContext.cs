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

        public DbSet<MyRestaurantApi.Contact> Contact { get; set; } = default!;
    }
}
