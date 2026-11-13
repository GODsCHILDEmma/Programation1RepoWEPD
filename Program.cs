List<int> ids = new List<int>();
Dictionary<int, string> names = new Dictionary<int, string>();
Dictionary<int, string> lastNames = new Dictionary<int, string>();
Dictionary<int, string> CoursesSelected = new Dictionary<int, string>();
Dictionary<int, int> age = new Dictionary<int, int>();

string CoursesAvailables = """
1.Informatica
2.Gastronomia
3.Turismo
4.Refrigeración
""";

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
                AddNewEstudiante(ids, names, lastNames, age, CoursesSelected, CoursesAvailables);
                break;
            case 2:
                EditCInsritos(ids, names, lastNames, age);
                break;
            case 3:
                ListAllInscritos(ids, names, lastNames, age, CoursesSelected);
                break;
            case 4:
                DeleteInscritos(ids, names, lastNames, age, CoursesSelected);
                break;
            case 5:
                ListAllCourses(CoursesAvailables);
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
        Console.WriteLine("=== AGENDA PERSONAL ===");
        Console.WriteLine("Sistema de gestión de contactos");
        Console.WriteLine("Desarrollado para organizar tu información de contactos");
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\n=== MENU PRINCIPAL  ===");
        Console.WriteLine("SELECCIONE UNA OPCION A CONTINUACION...");
        Console.WriteLine("1. Agregar nuevo estudiante");
        Console.WriteLine("2. Editar Inscritos");
        Console.WriteLine("3. Listar todos los Inscritos");
        Console.WriteLine("4. Eliminar Estudiante Inscritos");
        Console.WriteLine("5. Ver Cursos Disponibles");
        Console.WriteLine("6. Salir");
    }


    // Esta funcion convierte el numero del curso seleccionado en su nombre
    static string GetCourseName(int option)
    {
        switch (option)
        {
            case 1:
                return "Informatica";
            case 2:
                return "Gastronomia";
            case 3:
                return "Turismo";
            case 4:
                return "Refrigeración";
            default:
                return "Sin curso";
        }
    }


    static void AddNewEstudiante(List<int> ids,
        Dictionary<int, string> names,
        Dictionary<int, string> lastNames,
        Dictionary<int, int> ages,
        Dictionary<int, string> coursesSelected,
        string coursesAvailables
         )
    {
        Console.WriteLine("==Cursos Disponibles==");
        Console.WriteLine(coursesAvailables);
        Console.WriteLine("");
        Console.WriteLine("==AGREGAR UN ESTUDIANTE==");


        // Se usa el ID mas alto y despues se le suma uno para que no se repita (por si se elimino un estudiante)
        var id = ids.Count == 0 ? 1 : ids.Max() + 1;
        ids.Add(id);


        Console.WriteLine("");
        Console.WriteLine("Ingrese el Nombre ");
        var Name = Console.ReadLine();
        names.Add(id, Name);

        Console.WriteLine("");
        Console.WriteLine("Ingrese el Apellido");
        var LastName = Console.ReadLine();
        lastNames.Add(id, LastName);

        Console.WriteLine("");
        Console.WriteLine("Ingrese la Edad del Estudiante");
        var newAge = Convert.ToInt32(Console.ReadLine());
        ages.Add(id, newAge);

        // Con el While se repite la pregunta hasta que el numero del curso sea valido (1 al 4)
        Console.WriteLine("");
        Console.WriteLine("Seleccione el numero del Curso (1-4)");
        var courseOption = Convert.ToInt32(Console.ReadLine());

        while (courseOption < 1 || courseOption > 4)
        {
            Console.WriteLine("");
            Console.WriteLine("Curso no válido. Seleccione un numero del 1 al 4");
            courseOption = Convert.ToInt32(Console.ReadLine());
        }

        coursesSelected.Add(id, GetCourseName(courseOption));
        Console.WriteLine("");

        Console.WriteLine($"Estudiante Agregado Exitosamente con ID {id}");
    }

    static void EditCInsritos(List<int> ids,
        Dictionary<int, string> names,
        Dictionary<int, string> lastNames,
        Dictionary<int, int> ages
         )
    {
        Console.WriteLine("");
        Console.WriteLine("==EDITAR UN ESTUDIANTE==");

        if (ids.Count == 0)
        {
            Console.WriteLine("No hay estudiantes inscritos para editar.");
            return;
        }

        Console.WriteLine("");
        Console.WriteLine("Ingrese el ID del Estudiante a editar");
        var id = Convert.ToInt32(Console.ReadLine());

        if (!ids.Contains(id))
        {
            Console.WriteLine("");
            Console.WriteLine($"No existe ningún estudiante con ID {id}");
            return;
        }

        Console.WriteLine("");
        Console.WriteLine($"Datos actuales: {names[id]} {lastNames[id]}, {ages[id]} años");

        Console.WriteLine("");
        Console.WriteLine("Ingrese el nuevo Nombre ");
        var Name = Console.ReadLine();
        names[id] = Name;

        Console.WriteLine("");
        Console.WriteLine("Ingrese el nuevo Apellido");
        var LastName = Console.ReadLine();
        lastNames[id] = LastName;

        Console.WriteLine("");
        Console.WriteLine("Ingrese la nueva Edad del Estudiante");
        var newAge = Convert.ToInt32(Console.ReadLine());
        ages[id] = newAge;
        Console.WriteLine("");

        Console.WriteLine($"Estudiante con ID {id} Editado Exitosamente");
    }

    static void ListAllInscritos(List<int> ids,
        Dictionary<int, string> names,
        Dictionary<int, string> lastNames,
        Dictionary<int, int> ages,
        Dictionary<int, string> coursesSelected
         )
    {
        Console.WriteLine("");
        Console.WriteLine("==LISTA DE INSCRITOS==");

        if (ids.Count == 0)
        {
            Console.WriteLine("");
            Console.WriteLine("No hay estudiantes inscritos todavía.");
            return;
        }

        foreach (var id in ids)
        {
            Console.WriteLine("");
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Nombre: {names[id]}");
            Console.WriteLine($"Apellido: {lastNames[id]}");
            Console.WriteLine($"Edad: {ages[id]}");
            Console.WriteLine($"Curso: {coursesSelected[id]}");
        }

        Console.WriteLine("");
        Console.WriteLine($"Total de Inscritos: {ids.Count}");
    }

    static void DeleteInscritos(List<int> ids,
        Dictionary<int, string> names,
        Dictionary<int, string> lastNames,
        Dictionary<int, int> ages,
        Dictionary<int, string> coursesSelected
         )
    {
        Console.WriteLine("");
        Console.WriteLine("==ELIMINAR UN ESTUDIANTE==");

        if (ids.Count == 0)
        {
            Console.WriteLine("No hay estudiantes inscritos para eliminar.");
            return;
        }

        Console.WriteLine("");
        Console.WriteLine("Ingrese el ID del Estudiante a eliminar");
        var id = Convert.ToInt32(Console.ReadLine());

        if (!ids.Contains(id))
        {
            Console.WriteLine("");
            Console.WriteLine($"No existe ningún estudiante con ID {id}");
            return;
        }

        ids.Remove(id);
        names.Remove(id);
        lastNames.Remove(id);
        ages.Remove(id);
        coursesSelected.Remove(id);
        Console.WriteLine("");

        Console.WriteLine($"Estudiante con ID {id} Eliminado Exitosamente");
    }

    static void ListAllCourses(string coursesAvailables)
    {
        Console.WriteLine("");
        Console.WriteLine("==Cursos Disponibles==");
        Console.WriteLine(coursesAvailables);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Ocurrió un error: " + ex.Message);
}