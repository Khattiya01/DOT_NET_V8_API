using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Models;

namespace WebAppAPI.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shirt> Shirts { get; set; }
   /*     public DbSet<Company> Companys { get; set; }*/
        public DbSet<SaveManager> SaveManagers { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<SkillTree> SkillTrees { get; set; }
        public DbSet<Inventory> Inventorys { get; set; }
        public DbSet<CheckPoints> CheckPoints { get; set; }
        public DbSet<VolumeSettings> VolumeSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaveManager>()
               .Property(e => e.EmployeeId)
               .HasColumnType("uuid");

         /*   modelBuilder.Entity<Weapon>()
               .Property(e => e.EmployeeId)
               .HasColumnType("uuid");
*/
            modelBuilder.Entity<Company>()
               .Property(e => e.EmployeeId)
               .HasColumnType("uuid");

            modelBuilder.Entity<CheckPoints>()
                .HasOne(cp => cp.SaveManager)
                .WithOne(sm => sm.Checkpoints)
                .HasForeignKey<CheckPoints>(cp => cp.SaveManagerId);

            modelBuilder.Entity<Inventory>()
                .HasOne(inv => inv.SaveManager)
                .WithOne(sm => sm.Inventory)
                .HasForeignKey<Inventory>(inv => inv.SaveManagerId);

            modelBuilder.Entity<SkillTree>()
               .HasOne(sk => sk.SaveManager)
               .WithOne(sm => sm.SkillTree)
               .HasForeignKey<SkillTree>(sk => sk.SaveManagerId);

            modelBuilder.Entity<VolumeSettings>()
               .HasOne(vs => vs.SaveManager)
               .WithOne(sm => sm.VolumeSettings)
               .HasForeignKey<VolumeSettings>(vs => vs.SaveManagerId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
