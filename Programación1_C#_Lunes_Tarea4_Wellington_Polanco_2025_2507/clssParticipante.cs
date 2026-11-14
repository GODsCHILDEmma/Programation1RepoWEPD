using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrodeParticipantesCursos
{
    public class clssParticipante
    {
        public int Id { get; private set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }

        public clssParticipante(int id, string nombre,
            string apellido, int edad)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
        }

        public string NombreCompleto()
        {
            return Nombre + " " + Apellido;
        }
    }
}