using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantService.Api;

namespace RestaurantService.Api.Data
{
    public class RestaurantServiceApiContext : DbContext
    {
        public RestaurantServiceApiContext (DbContextOptions<RestaurantServiceApiContext> options)
            : base(options)
        {
        }

        public DbSet<RestaurantService.Api.Contact> Contact { get; set; } = default!;

        public DbSet<RestaurantService.Api.MenuItem> MenuItem { get; set; } = default!;

        public DbSet<RestaurantService.Api.TogoOrder> TogoOrder { get; set; } = default!;
    }
}
