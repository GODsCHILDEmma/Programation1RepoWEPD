using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrodeParticipantesCursos
{
    public class clssInscripcion
    {
       private clssInscripcion()
        
        {
            Participante = null!;
            Curso = null!;
        }

         public int Id { get; private set; }
         public clssParticipante Participante { get; set; }
         public clssCurso Curso { get; set; }

        public clssInscripcion(int id,
            clssParticipante participante,
            clssCurso curso)
        {
            Id = id;
            Participante = participante;
            Curso = curso;
        }
    }
}