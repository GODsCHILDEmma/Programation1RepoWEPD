
using Microsoft.EntityFrameworkCore;
using RegistrodeParticipantesCursos;

try
{
    
    using (RegistroContext db = new RegistroContext()) // Un IF para Comprobar la conexion con SQL Server
    {
        if (db.Database.CanConnect())
        {
            Console.WriteLine("Conexion exitosa con SQL Server.");
        }
        else
        {
            Console.WriteLine("No se pudo conectar con SQL Server.");
            return;
        }
    }

    DisplayHeader();

    bool wantToContinue = true;

    while (wantToContinue)
    {
        DisplayMenu();
        int chosenOption = Convert.ToInt32(Console.ReadLine());

        // Se limpia la pantalla despues de cada seleccion
        Console.Clear();

        switch (chosenOption)
        {
            case 1:
                AddNewEstudiante();
                break;
            case 2:
                EditCInsritos();
                break;
            case 3:
                ListAllInscritos();
                break;
            case 4:
                DeleteInscritos();
                break;
            case 5:
                ListAllCourses();
                break;
            case 6:
                wantToContinue = false;
                Console.WriteLine("Cerrando la aplicación...");
                Console.WriteLine("Presione cualquier tecla para salir...");
                Console.ReadKey();
                break;
            default:
                Console.WriteLine("Opción no válida. Intente nuevamente.");
                break;
        }
    }

    static void DisplayHeader()
    {
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("=== REGISTRO DE PARTICIPANTES ===");
        Console.WriteLine("Sistema de inscripción para cursos");
        Console.WriteLine("Administración de participantes y cursos");
    }

    static void DisplayMenu()
    {
        
        Console.WriteLine("\n=== MENU PRINCIPAL  ===");
        Console.WriteLine("SELECCIONE UNA OPCION A CONTINUACION...");
        Console.WriteLine("1. Agregar nuevo estudiante");
        Console.WriteLine("2. Editar Inscritos");
        Console.WriteLine("3. Listar todos los Inscritos");
        Console.WriteLine("4. Eliminar Estudiante Inscrito");
        Console.WriteLine("5. Ver Cursos Disponibles");
        Console.WriteLine("6. Salir");
    }

    static void AddNewEstudiante()
    {
        Console.WriteLine("");
        Console.WriteLine("==AGREGAR UN ESTUDIANTE==");

        using (RegistroContext db = new RegistroContext())
        {
            Console.WriteLine("");
            Console.WriteLine("Ingrese el Nombre");
            var Name = Console.ReadLine() ?? "no Name...";

            Console.WriteLine("");
            Console.WriteLine("Ingrese el Apellido");
            var LastName = Console.ReadLine() ?? "no Last Name...";

            Console.WriteLine("");
            Console.WriteLine("Ingrese la Edad del Estudiante");
            var newAge = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("---Cursos Disponibles Actualmente---");
            ListAllCourses();

            Console.WriteLine("");
            Console.WriteLine("Seleccione el numero del Curso");
            var courseOption = Convert.ToInt32(Console.ReadLine());

            clssCurso? cursoSelected = db.Cursos
                .FirstOrDefault(c => c.Id == courseOption);

            // Repite hasta seleccionar un curso valido
            while (cursoSelected == null)
            {
                Console.WriteLine("");
                Console.WriteLine("Curso no valido. Seleccione un ID de la lista.");
                courseOption = Convert.ToInt32(Console.ReadLine());

                cursoSelected = db.Cursos
                    .FirstOrDefault(c => c.Id == courseOption);
            }

           
            clssParticipante nuevoEstudiante = new clssParticipante(
                0, Name, LastName, newAge);

            db.Participantes.Add(nuevoEstudiante);

           
            clssInscripcion nuevaInscripcion = new clssInscripcion(
                0, nuevoEstudiante, cursoSelected);

            db.Inscripciones.Add(nuevaInscripcion);

            
            db.SaveChanges(); // Guardar la informacion y cambios en SQL Server

            Console.WriteLine("");
            Console.WriteLine("Estudiante Agregado Exitosamente");
            Console.WriteLine("ID del Estudiante: " + nuevoEstudiante.Id);
        }
    }

    static void EditCInsritos()
    {
        Console.WriteLine("");
        Console.WriteLine("==EDITAR UN ESTUDIANTE==");

        using (RegistroContext db = new RegistroContext())
        {
            if (db.Participantes.Count() == 0)
            {
                Console.WriteLine("No hay estudiantes inscritos para editar.");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("Ingrese el ID del Estudiante a editar");
            var id = Convert.ToInt32(Console.ReadLine());

            clssParticipante? estudiante = db.Participantes
                .FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
            {
                Console.WriteLine("");
                Console.WriteLine($"No existe ningún estudiante con ID {id}");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine($"Editando a: {estudiante.NombreCompleto()}");

            Console.WriteLine("");
            Console.WriteLine("Ingrese el nuevo Nombre");
            var Name = Console.ReadLine() ?? "";

            Console.WriteLine("");
            Console.WriteLine("Ingrese el nuevo Apellido");
            var LastName = Console.ReadLine() ?? "";

            Console.WriteLine("");
            Console.WriteLine("Ingrese la nueva Edad del Estudiante");
            var newAge = Convert.ToInt32(Console.ReadLine());

            estudiante.Nombre = Name;
            estudiante.Apellido = LastName;
            estudiante.Edad = newAge;

            db.SaveChanges();

            Console.WriteLine("");
            Console.WriteLine($"Estudiante con ID {id} Editado Exitosamente");
        }
    }

    static void ListAllInscritos()
    {
        Console.WriteLine("");
        Console.WriteLine("==LISTA DE INSCRITOS==");

        using (RegistroContext db = new RegistroContext())
        {
            List<clssInscripcion> inscripcionesList = db.Inscripciones
                .Include(i => i.Participante)
                .Include(i => i.Curso)
                .ToList();

            if (inscripcionesList.Count == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("No hay estudiantes inscritos aún.");
                return;
            }

            foreach (clssInscripcion inscripcion in inscripcionesList)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine($"ID: {inscripcion.Participante.Id}");
                Console.WriteLine($"Nombre: {inscripcion.Participante.Nombre}");
                Console.WriteLine($"Apellido: {inscripcion.Participante.Apellido}");
                Console.WriteLine($"Edad: {inscripcion.Participante.Edad}");
                Console.WriteLine($"Curso: {inscripcion.Curso.Nombre}");
            }

            Console.WriteLine("");
            Console.WriteLine($"Total de Inscritos: {inscripcionesList.Count}");
        }
    }

    static void DeleteInscritos()
    {
        Console.WriteLine("");
        Console.WriteLine("==ELIMINAR UN ESTUDIANTE==");

        using (RegistroContext db = new RegistroContext())
        {
            if (db.Participantes.Count() == 0)
            {
                Console.WriteLine("No hay estudiantes inscritos para eliminar.");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("Ingrese el ID del Estudiante a eliminar");
            var id = Convert.ToInt32(Console.ReadLine());

            clssParticipante? estudiante = db.Participantes
                .FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
            {
                Console.WriteLine("");
                Console.WriteLine($"No existe ningún estudiante con ID {id}");
                return;
            }

            // Buscar en la base de datos inscripciones del estudiante
            List<clssInscripcion> inscripcionesList = db.Inscripciones
                .Where(i => i.Participante.Id == id)
                .ToList();

            // Eliminar primero las inscripciones
            db.Inscripciones.RemoveRange(inscripcionesList);

            // Eliminar y despues el participante
            db.Participantes.Remove(estudiante);

            db.SaveChanges();

            Console.WriteLine("");
            Console.WriteLine($"Estudiante con ID {id} Eliminado Exitosamente");
        }
    }

    static void ListAllCourses()
    {
        Console.WriteLine("");
        Console.WriteLine("===Cursos Disponibles===");

        using (RegistroContext db = new RegistroContext())
        {
            List<clssCurso> cursosList = db.Cursos.ToList();

            foreach (clssCurso curso in cursosList)
            {
                Console.WriteLine($"{curso.Id}. {curso.Nombre}");
            }
        }
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine("");
    Console.WriteLine("Has introducido un caracter no valido: " + ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine("");
    Console.WriteLine("Ocurrió un error: " + ex.Message);
}
finally
{
    Console.Clear();
    Console.WriteLine("");
    Console.WriteLine("Proceso Finalizado.");
}
