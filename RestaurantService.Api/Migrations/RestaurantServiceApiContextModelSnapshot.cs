// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantService.Api.Data;

#nullable disable

namespace RestaurantService.Api.Migrations
{
    [DbContext(typeof(RestaurantServiceApiContext))]
    partial class RestaurantServiceApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("RestaurantService.Api.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("RestaurantService.Api.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MenuItem");
                });

            modelBuilder.Entity("RestaurantService.Api.MenuItemOrdered", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("TogoOrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TogoOrderId");

                    b.ToTable("MenuItemOrdered");
                });

            modelBuilder.Entity("RestaurantService.Api.TogoOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("OrderCreated")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PaymentMethod")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Subtotal")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Tax")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Total")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("TogoOrder");
                });

            modelBuilder.Entity("RestaurantService.Api.MenuItemOrdered", b =>
                {
                    b.HasOne("RestaurantService.Api.TogoOrder", null)
                        .WithMany("ItemsOrdered")
                        .HasForeignKey("TogoOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantService.Api.TogoOrder", b =>
                {
                    b.HasOne("RestaurantService.Api.Contact", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("RestaurantService.Api.TogoOrder", b =>
                {
                    b.Navigation("ItemsOrdered");
                });
#pragma warning restore 612, 618
        }
    }
}
