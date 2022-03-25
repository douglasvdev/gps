using API.Models;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
//using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace API.Data
{
    public class Contexto : DbContext
    {
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        //public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Scout> Scouts { get; set; }

        public Contexto()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RP3P6NN; initial Catalog=GPS; User ID=sa; Password=A852889!")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
