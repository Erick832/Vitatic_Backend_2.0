using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitatic.Entities;

namespace Vitatic.DataAccess
{
    public class VitaticDbContext:DbContext
    {
        public VitaticDbContext()
        {

        }
        public VitaticDbContext(DbContextOptions<VitaticDbContext>options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Interface> Interfaces { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<AdviceSection> AdviceSections { get; set; }
        public DbSet<Advice> Advices { get; set; }
        public DbSet<Instruction> Instructions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(b => b.Interface)
                .WithOne(i => i.User)
                .HasForeignKey<Interface>(b => b.UserId);
            
            modelBuilder.Entity<Interface>()
                .HasOne(b => b.Schedule)
                .WithOne(i => i.Interface)
                .HasForeignKey<Schedule>(b => b.InterfaceId);

            modelBuilder.Entity<Activity>()
                .HasOne(b => b.Progress)
                .WithOne(i => i.Activity)
                .HasForeignKey<Progress>(b => b.Id);

            modelBuilder.Entity<Interface>()
                .HasOne(b => b.AdviceSection)
                .WithOne(i => i.Interface)
                .HasForeignKey<AdviceSection>(b => b.InterfaceId);

            /*modelBuilder.Entity<Interface>()
                .HasOne(b => b.Instruction)
                .WithOne(i => i.Interface)
                .HasForeignKey<Instruction>(b => b.InterfaceId);*/

        }
    }
}
