using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.Models;

public partial class DbMSContextNew : DbContext
{
    public DbMSContextNew()
    {
    }

    public DbMSContextNew(DbContextOptions<DbMSContextNew> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__Carts__8BA3E887372E4F4B");

            entity.Property(e => e.IdCart).HasColumnName("idCart");
            entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carts__idCustome__534D60F1");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => e.IdCartDetail).HasName("PK__CartDeta__E29EFB83EF337BB8");

            entity.ToTable("CartDetail");

            entity.Property(e => e.IdCartDetail).HasColumnName("idCartDetail");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.IdCart).HasColumnName("idCart");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.IdCart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartDetai__idCar__5441852A");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartDetai__idPro__5535A963");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Categori__79D361B64AF70104");

            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__Customer__D0587686C9E2F81F");

            entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .HasColumnName("displayName");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("123456")
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Products__5EEC79D1A1CAA6BE");

            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__idCate__52593CB8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
