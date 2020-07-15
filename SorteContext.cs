using Microsoft.EntityFrameworkCore;
using Sorte.Lib;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sorte.Dal
{
    public class SorteContext : DbContext
    {

        public SorteContext()
        { }

        public SorteContext(DbContextOptions<SorteContext> options) 
            : base(options)
        { }

        public DbSet<Megasena> Megasenas { get; set; }
        public DbSet<Sorteados> Sorteados { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var now = DateTime.UtcNow;

            builder.Entity<Sorteados>().HasOne(s => s.MegaSena).WithMany(m => m.NumerosSorteados); ;
            builder.Entity<Megasena>();

        }

        //public override int SaveChanges(bool acceptAllChangesOnSuccess)
        //{
        //    this.OnBeforeSaving();
        //    return base.SaveChanges(acceptAllChangesOnSuccess);
        //}

        //public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    this.OnBeforeSaving();
        //    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}

        //private void OnBeforeSaving()
        //{
        //    var entries = ChangeTracker.Entries();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=EFCoreDemo;Trusted_Connection=True;MultipleActiveResultSets=true");
        }


    }
}
