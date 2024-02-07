using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TradingBot.Models;

public partial class TradingBotDbContext : DbContext
{
    public TradingBotDbContext()
    {
    }

    public TradingBotDbContext(DbContextOptions<TradingBotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<UserFav> UserFavs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=.\\Database\\TradingBotDb.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.HasIndex(e => e.Id, "IX_Client_Id").IsUnique();

            entity.HasIndex(e => e.Login, "IX_Client_Login").IsUnique();
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");

            entity.HasIndex(e => e.Id, "IX_Item_Id").IsUnique();

            entity.HasOne(d => d.Type).WithMany(p => p.Items)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.ToTable("Type");

            entity.HasIndex(e => e.Id, "IX_Type_Id").IsUnique();
        });

        modelBuilder.Entity<UserFav>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_UserFavs_Id").IsUnique();

            entity.HasOne(d => d.Client).WithMany(p => p.UserFavs)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Item).WithMany(p => p.UserFavs)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
