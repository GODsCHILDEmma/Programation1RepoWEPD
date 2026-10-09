using System.Xml.Schema;
bool KeepGoing = true;

do 
    {
        try
        {
            Console.WriteLine("");
            Console.WriteLine("---Bienvenidos a La Calculadora---");
            Console.WriteLine("Esta Calculadora puede calcular 2 o mas números...");
            Console.WriteLine("");
            Console.WriteLine("Seleccione la opción a elegir:");
            Console.WriteLine("");
            Console.WriteLine("1.Suma");
            Console.WriteLine("2.Resta");
            Console.WriteLine("3.Multiplicación");
            Console.WriteLine("4.División");
            Console.WriteLine("5.Salir");
            Console.WriteLine("");
            Console.WriteLine("Digite la opción que desea (1-5):");
            int SelectOption = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");

            if (SelectOption < 1 || SelectOption > 5)
            {
                Console.WriteLine("Error...seleccione una opcion valida");
                return;
            }
            else if (SelectOption == 5) 
            {
                Console.WriteLine("Saliendo del Programa...");
                KeepGoing = false;
                break;
            }

            // decimal[] NumberScritos = new decimal[2]; // Creé un arreglo que almacena los dos primeros numeros que el usuario ingresa.
            List<decimal> NumberScritos = new List<decimal>();

            Console.WriteLine("Ingrese el primer numero:");
            NumberScritos.Add(Convert.ToDecimal(Console.ReadLine()));//Aqui declaramos las variables en las posiciones que quiero que/recibo los datos del usuario
            Console.WriteLine("");

            Console.WriteLine("Ingrese el segundo numero:");
            NumberScritos.Add(Convert.ToDecimal(Console.ReadLine()));
            Console.WriteLine("");

            int WantToContinuar = 0;
            while (WantToContinuar != 2)
            {
                Console.WriteLine("Quieres añadir otro número ?");
                Console.WriteLine("1. Si");
                Console.WriteLine("2. No, Calculemos ya!");
                Console.WriteLine("Su opción: ");
                WantToContinuar = Convert.ToInt32(Console.ReadLine());

                if (WantToContinuar == 1)
                {
                    Console.WriteLine("Digite el nuevo numero a calcular");
                    NumberScritos.Add(Convert.ToDecimal(Console.ReadLine()));
                }
                else if (WantToContinuar != 2)
                {
                    Console.WriteLine("Error: valor no valido");
                    WantToContinuar = 0;
                }
            }
            decimal result = 0;
            string operationNombre = "";


            switch (SelectOption)
            {
                case 1:
                    {
                        operationNombre = "Suma";
                        for (int i = 1; i < NumberScritos.Count; i++)
                        {
                            result += NumberScritos[i];
                        }

                    }
                    break;

                case 2:
                    {
                        operationNombre = "Resta";
                        foreach (decimal NumberScrito in NumberScritos)
                        {
                            result -= NumberScrito;
                        }
                    }
                    break;

                case 3:
                    {
                        operationNombre = "Multiplicación";
                        result = 1;
                        foreach (decimal NumberScrito in NumberScritos)
                        {
                            result *= NumberScrito;
                        }

                    }
                    break;

                case 4:
                    {
                        operationNombre = "Division";
                        for (int i = 1; i < NumberScritos.Count; i++)
                        {
                            result /= NumberScritos[i];
                        }


                    }
                    break;

                case 5:
                    {
                        KeepGoing = false;

                    }
                    break;

            }

            //Imprimir Resultados del calculo 
            Console.WriteLine($"El resultado de la {operationNombre} es: {result}");
            Console.WriteLine($"Estos fueron los números que utilizaste: [{string.Join(",", NumberScritos)}] ");



        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Por favor ingrese solo números válidos");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: División por cero no permitida");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    } while (KeepGoing);

