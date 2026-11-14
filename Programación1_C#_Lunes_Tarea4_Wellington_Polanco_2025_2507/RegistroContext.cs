using Microsoft.EntityFrameworkCore;

namespace RegistrodeParticipantesCursos
{
    public class RegistroContext : DbContext
    {
        public DbSet<clssParticipante> Participantes { get; set; } = null!;
        public DbSet<clssCurso> Cursos { get; set; } = null!;
        public DbSet<clssInscripcion> Inscripciones { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=RegistroParticipantesDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}



