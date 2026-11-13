namespace RegistrodeParticipantesCursos
{
    public class clssCurso
    {
        public int Id { get; private set; }
        public string Nombre { get; set; }

        public clssCurso(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}