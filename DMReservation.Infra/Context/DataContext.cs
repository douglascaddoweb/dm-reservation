using DMReservation.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DMReservation.Infra.Context
{
    public class DataContext : DbContext
    {
        //public DbSet<Pessoa> Pessoas { get; set; }
        

        public DataContext(DbContextOptions option) : base(option)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, (_, level) => level == LogLevel.Information)
                .UseNpgsql(GeneralSetting.ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

    }
}
