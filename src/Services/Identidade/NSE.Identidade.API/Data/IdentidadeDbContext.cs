using Microsoft.EntityFrameworkCore;
using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Data
{
    public class IdentidadeDbContext : DbContext
    {
        public IdentidadeDbContext(DbContextOptions<IdentidadeDbContext> options) : base(options) {  }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
