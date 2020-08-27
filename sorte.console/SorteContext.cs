using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace sorte.console
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
        public DbSet<MegasenaCidade> MegasenaCidades { get; set; }
        public DbSet<EstatisticaMegasena> EstatisticaMegasenas { get; set; }
        public DbSet<MegasenaCounter> MegasenaCounters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var now = DateTime.UtcNow;

            builder.Entity<Sorteados>().HasOne(s => s.MegaSena).WithMany(m => m.NumerosSorteados);
            builder.Entity<Megasena>().HasMany(n => n.NumerosSorteados).WithOne(n => n.MegaSena);
            builder.Entity<MegasenaCidade>().HasOne(c => c.MegasenaConcurso).WithMany(m => m.Cidades);
            builder.Entity<Megasena>().HasMany(c => c.Cidades).WithOne(m => m.MegasenaConcurso);
            builder.Entity<EstatisticaMegasena>();
            builder.Entity<MegasenaCounter>();

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                optionsBuilder.UseSqlServer(@"Server=.\;Database=Sorte;Trusted_Connection=True;MultipleActiveResultSets=true"); // windows
            }
            else
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=Sorte;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=SA;Password=L0g1t3ch#1972;"); // Mac
            }
            
        }


    }
}
