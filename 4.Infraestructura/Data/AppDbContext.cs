using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    public override int SaveChanges()
    {
        // Cambia todas las fechas a UTC antes de guardar
        foreach (var entry in ChangeTracker.Entries<Tarea>())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.FechaCreacion = entry.Entity.FechaCreacion.ToUniversalTime();
            }
        }
        return base.SaveChanges();
    }
}
