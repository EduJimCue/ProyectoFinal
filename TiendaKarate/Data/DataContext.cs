using Microsoft.EntityFrameworkCore;
using TiendaKarate.Models;
using System;

namespace TiendaKarate.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Age> Ages { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<SizeProduct> SizeProducts { get; set; }
        public DbSet<ImageProduct> ImageProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Email = "admin@something.com", Name = "admin", Password = "1234", Role = true, SignUpDate = DateTime.Now, Adress = "Avenida de Madrid 24 2ºA", Id = 1 }
            );
            modelBuilder.Entity<Team>().HasData(
                new Team { Name = "Sankukai", Password = "1234", IsPro = true, Id = 1 }
            );
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Name = "Adidas", Id = 1 }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category { Name = "Kata", Id = 1 }
            );
            modelBuilder.Entity<Colour>().HasData(
                new Colour { Name = "Blanco", Id = 1 }
            );
            modelBuilder.Entity<Size>().HasData(
                new Size { Name = "M", Id = 1 },
                new Size { Name = "L", Id = 2 },
                new Size { Name = "XL", Id = 3 }
            );
            modelBuilder.Entity<Sport>().HasData(
                new Sport { Name = "Karate", Id = 1 }
            );
            modelBuilder.Entity<Age>().HasData(
                new Age { Name = "Adulto", Id = 1 },
                new Age { Name = "Niño", Id = 2 }
            );
            modelBuilder.Entity<Gender>().HasData(
                new Gender { Name = "Unisex", Id = 1 },
                new Gender { Name = "Hombre", Id = 2 },
                new Gender { Name = "Mujer", Id = 3 }
            );
            modelBuilder.Entity<Image>().HasData(
                new Image { Url = "https://tiendakarate.s3.amazonaws.com/imagenes/karategiAdidas.png", Name = "katategiAdidas", Id = 1 }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { Name = "Karategi Adidas Kata Nostalgia", Price = 60, SportId = 1, BrandId = 1, CategoryId = 1, ColourId = 1, TeamId = 1, Id = 1, AgeId = 1, GenderId = 1, IsActive = true, PrincipalImage = "https://tiendakarate.s3.amazonaws.com/imagenes/karategiAdidas.png",SecondImage="https://tiendakarate.s3.amazonaws.com/imagenes/karategiAdidas.png",ThirdImage="https://tiendakarate.s3.amazonaws.com/imagenes/karategiAdidas.png",FourthImage="https://tiendakarate.s3.amazonaws.com/imagenes/karategiAdidas.png", Description="Recuerda tus mejores momentos de competion con este kimono que refleja los valores de tus años dorados" }
            );
            modelBuilder.Entity<Order>().HasData(
                new Order {Id = 1, UserId = 1, CreationDate = DateTime.Now, Paid = false}
            );
             modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct {Id = 1, OrderId = 1, SizeProductId = 1001, Quantity = 1},
                new OrderProduct {Id = 2, OrderId = 1, SizeProductId = 1002, Quantity = 1}
            );
            modelBuilder.Entity<SizeProduct>().HasData(
                new SizeProduct {Id = 1, SizeId = 1, ProductId = 1, Stock =30},
                new SizeProduct {Id = 2, SizeId = 2, ProductId = 1, Stock=25}
            );
            modelBuilder.Entity<ImageProduct>().HasData(
                new ImageProduct {Id = 1, ImageId = 1, ProductId = 1}
            );
        }
    }
}
