using FirstOne.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstOne.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }

        public DbSet<Empregado> Empregados { get; set; }
        public DbSet<Departamento> departamentos { get; set; }
        public object Departamentos { get; internal set; }
    }
}
