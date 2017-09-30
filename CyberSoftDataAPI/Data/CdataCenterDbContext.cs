using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using CyberSoftDataCenter.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using CyberSoftDataCenter.Models.AccountViewModels;

namespace CyberSoftDataCenter.Data
{
    public class CdataCenterDbContext : DbContext
    {
        public CdataCenterDbContext(DbContextOptions<CdataCenterDbContext> options) : base(options)
        {
        }
        public CdataCenterDbContext() : base()
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UsersRoles> UsersRoles { get; set; }
        public DbSet<Attributs> Attributs { get; set; }
        public DbSet<RolesAttributs> RolesAttributs { get; set; }
        public DbSet<Pays> Pays { get; set; }
        public DbSet<Villes> Villes { get; set; }

        public DbSet<CybersCenters> CybersCenters { get; set; }
        public DbSet<VenteUnites> VenteUnites { get; set; }
        public DbSet<VenteProduits> VenteProduits { get; set; }
        public DbSet<DetaillesVentes> DetaillesVentes { get; set; }
        public DbSet<BkpDataBases> BkpDataBases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<UsersRoles>().ToTable("UsersRoles");
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<Attributs>().ToTable("Attributs");
            modelBuilder.Entity<RolesAttributs>().ToTable("RolesAttributs");
            modelBuilder.Entity<Villes>().ToTable("Villes")
                .HasOne(e => e.Pays)
                .WithMany(b => b.Villes)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CybersCenters>().ToTable("CybersCenters")
                .HasOne(e => e.Villes)
                .WithMany(b => b.CybersCenters)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VenteUnites>().ToTable("VenteUnites");
               

            modelBuilder.Entity<Pays>().ToTable("Pays");

            modelBuilder.Entity<VenteProduits>().ToTable("VenteProduits");


            modelBuilder.Entity<DetaillesVentes>().ToTable("DetaillesVentes");
            modelBuilder.Entity<BkpDataBases>().ToTable("BkpDataBases");


        }

        public DbSet<CyberSoftDataCenter.Models.AccountViewModels.RolesListViewModel> RolesListViewModel { get; set; }

        public DbSet<CyberSoftDataCenter.Models.AccountViewModels.RoleViewModel> RoleViewModel { get; set; }

    }
}
