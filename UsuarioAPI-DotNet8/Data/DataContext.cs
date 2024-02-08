using Microsoft.EntityFrameworkCore;
using UsuarioAPI_DotNet8.Entities;

namespace UsuarioAPI_DotNet8.Data
{
    public class DataContext : DbContext
    {
        public  DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
