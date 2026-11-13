
using RegistrodeParticipantesCursos;
using System.ComponentModel.Design;

List<clssParticipante> estudiantesList = new List<clssParticipante>();
List<clssCurso> cursosList = new List<clssCurso>();
List<clssInscripcion> inscripcionesList = new List<clssInscripcion>();

// Cursos añadidos a la clase Curso
cursosList.Add(new clssCurso(1, "Informatica"));
cursosList.Add(new clssCurso(2, "Gastronomia"));
cursosList.Add(new clssCurso(3, "Turismo"));
cursosList.Add(new clssCurso(4, "Refrigeración"));

try
{
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
                AddNewEstudiante(estudiantesList, cursosList, inscripcionesList);
                break;
            case 2:
                EditCInsritos(estudiantesList);
                break;
            case 3:
                ListAllInscritos(inscripcionesList);
                break;
            case 4:
                DeleteInscritos(estudiantesList, inscripcionesList);
                break;
            case 5:
                ListAllCourses(cursosList);
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

    static void AddNewEstudiante(
        List<clssParticipante> estudiantesList,
        List<clssCurso> cursosList,
        List<clssInscripcion> inscripcionesList)
    {
        Console.WriteLine("");
        Console.WriteLine("==AGREGAR UN ESTUDIANTE==");

        int id = estudiantesList.Count == 0
            ? 1
            : estudiantesList.Max(e => e.Id) + 1;

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
        ListAllCourses(cursosList);

        // Repite hasta seleccionar un curso valido
        Console.WriteLine("");
        Console.WriteLine("Seleccione el numero del Curso (1-4)");
        var courseOption = Convert.ToInt32(Console.ReadLine());

        while (courseOption < 1 || courseOption > 4)
        {
            Console.WriteLine("");
            Console.WriteLine("Curso no válido. Seleccione un numero del 1 al 4");
            courseOption = Convert.ToInt32(Console.ReadLine());
        }

        clssCurso? cursoSelected = cursosList
            .FirstOrDefault(c => c.Id == courseOption);

        if (cursoSelected == null)
        {
            Console.WriteLine("Curso no encontrado.");
            return;
        }

        // Crear participante
        clssParticipante nuevoEstudiante = new clssParticipante(
            id, Name, LastName, newAge);

        estudiantesList.Add(nuevoEstudiante);

        // Crear inscripción
        int idInscripcion = inscripcionesList.Count == 0
            ? 1
            : inscripcionesList.Max(i => i.Id) + 1;

        clssInscripcion nuevaInscripcion = new clssInscripcion(
            idInscripcion, nuevoEstudiante, cursoSelected);

        inscripcionesList.Add(nuevaInscripcion);

        Console.WriteLine("");
        Console.WriteLine("Estudiante Agregado Exitosamente");
    }

    static void EditCInsritos(List<clssParticipante> estudiantesList)
    {
        Console.WriteLine("");
        Console.WriteLine("==EDITAR UN ESTUDIANTE==");

        if (estudiantesList.Count == 0)
        {
            Console.WriteLine("No hay estudiantes inscritos para editar.");
            return;
        }

        Console.WriteLine("");
        Console.WriteLine("Ingrese el ID del Estudiante a editar");
        var id = Convert.ToInt32(Console.ReadLine());

        clssParticipante? estudiante = estudiantesList
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

        Console.WriteLine("");
        Console.WriteLine($"Estudiante con ID {id} Editado Exitosamente");
    }

    static void ListAllInscritos(List<clssInscripcion> inscripcionesList)
    {
        Console.WriteLine("");
        Console.WriteLine("==LISTA DE INSCRITOS==");

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

    static void DeleteInscritos(
        List<clssParticipante> estudiantesList,
        List<clssInscripcion> inscripcionesList)
    {
        Console.WriteLine("");
        Console.WriteLine("==ELIMINAR UN ESTUDIANTE==");

        if (estudiantesList.Count == 0)
        {
            Console.WriteLine("No hay estudiantes inscritos para eliminar.");
            return;
        }

        Console.WriteLine("");
        Console.WriteLine("Ingrese el ID del Estudiante a eliminar");
        var id = Convert.ToInt32(Console.ReadLine());

        clssParticipante? estudiante = estudiantesList
            .FirstOrDefault(e => e.Id == id);

        if (estudiante == null)
        {
            Console.WriteLine("");
            Console.WriteLine($"No existe ningún estudiante con ID {id}");
            return;
        }

        clssInscripcion? inscripcion = inscripcionesList
            .FirstOrDefault(i => i.Participante.Id == id);

        if (inscripcion != null)
        {
            inscripcionesList.Remove(inscripcion);
        }

        estudiantesList.Remove(estudiante);

        Console.WriteLine("");
        Console.WriteLine($"Estudiante con ID {id} Eliminado Exitosamente");
    }

    static void ListAllCourses(List<clssCurso> cursosList)
    {
        Console.WriteLine("");
        Console.WriteLine("===Cursos Disponibles===");

        foreach (clssCurso curso in cursosList)
        {
            Console.WriteLine($"{curso.Id}. {curso.Nombre}");
        }
    }
}
catch(ArgumentException ex) 
{
    Console.WriteLine("");
    Console.WriteLine("Haz introduciod un caracter no valido" + ex.Message);
}

catch (Exception ex)
{
    Console.WriteLine("");
    Console.WriteLine("Ocurrió un error: " + ex.Message);
}
finally
    {
    Console.WriteLine("");
    Console.WriteLine("Proceso Finalizado.");
    }