using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicio_14
{
    internal class Program
    {
        static int max_estudiantes = 40;
        static int max_cursos = 4;
        static string[] nombre_de_curos = { "matemáticas", "Programación", "Base de datos", "Física" };
        static string[] estudiantes = new string[max_estudiantes];
        static double[,] matriznotas = new double[max_estudiantes, max_cursos];
        static int total_estudiantes = 0;
        static string rutaArchivo = "notas_cursos.txt";

        public static void RegistrarEstudiantes()
        {
            Console.Clear();
            Console.WriteLine("--- Registro de estudiantes ---");
            if (total_estudiantes >= max_estudiantes)
            {
                Console.WriteLine("Capacidad máxima de estudiantes alcanzada");
                Console.ReadKey();
                return;
            }
            string nombre = Validador.LeerTexto("Ingresar nombres y apellidos: ").ToUpper();
            estudiantes[total_estudiantes] = nombre;
            Console.WriteLine("Ingresar notas para cada curso (0 a 20):");

            for (int i = 0; i < max_cursos; i++)
            {
                matriznotas[total_estudiantes, i] = Validador.LeerDecimales($"{nombre_de_curos[i]}: ");
            }

            total_estudiantes++;
            Console.WriteLine("Estudiante registrado..!");
            Console.ReadKey();
        }

        static void GuardarArchivo()
        {
            Console.Clear();
            Console.WriteLine("--- GUARDAR ARCHIVO ---");
            if (total_estudiantes == 0)
            {
                Console.WriteLine("No hay registros por guardar.");
                Console.ReadKey();
                return;
            }

            try
            {
                using (StreamWriter escribir = new StreamWriter(rutaArchivo))
                {
                    for (int i = 0; i < total_estudiantes; i++)
                    {
                        string linea = estudiantes[i];
                        for (int j = 0; j < max_cursos; j++)
                        {
                            linea += $";{matriznotas[i, j]}";
                        }
                        escribir.WriteLine(linea);
                    }
                }
                Console.WriteLine("Registros guardados correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar: {ex.Message}");
            }
            Console.ReadKey();
        }

        public static void CargarRegistros()
        {
            Console.Clear();
            Console.WriteLine("--- Cargar registros desde archivo ---");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("El archivo de registros no existe todavía.");
                Console.ReadKey();
                return;
            }

            try
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);
                total_estudiantes = 0;

                foreach (string linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] partes = linea.Split(';');
                    if (partes.Length >= max_cursos + 1)
                    {
                        estudiantes[total_estudiantes] = partes[0];
                        for (int j = 0; j < max_cursos; j++)
                        {
                            if (double.TryParse(partes[j + 1], out double nota))
                            {
                                matriznotas[total_estudiantes, j] = nota;
                            }
                        }
                        total_estudiantes++;
                    }
                }
                Console.WriteLine("Registros cargados correctamente desde el archivo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
            }
            Console.ReadKey();
        }

        public static void MostrarRegistros()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE ESTUDIANTES Y PROMEDIOS ===");
            if (total_estudiantes == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < total_estudiantes; i++)
            {
                Console.WriteLine($"\nEstudiante [{i + 1}]: {estudiantes[i]}");
                double suma = 0;
                for (int j = 0; j < max_cursos; j++)
                {
                    Console.WriteLine($"  - {nombre_de_curos[j]}: {matriznotas[i, j]}");
                    suma += matriznotas[i, j];
                }
                double promedio = suma / max_cursos;
                Console.WriteLine($"  => Promedio final: {promedio:F2}");
                Console.WriteLine("----------------------------------------");
            }
            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE NOTAS ===");
                Console.WriteLine("1. Registrar Estudiante");
                Console.WriteLine("2. Guardar en Archivo");
                Console.WriteLine("3. Cargar desde Archivo");
                Console.WriteLine("4. Mostrar Registros y Promedios");
                Console.WriteLine("5. Salir");

                opcion = Validador.LeerEnteros("Seleccione una opción: ");

                switch (opcion)
                {
                    case 1:
                        RegistrarEstudiantes();
                        break;
                    case 2:
                        GuardarArchivo();
                        break;
                    case 3:
                        CargarRegistros();
                        break;
                    case 4:
                        MostrarRegistros();
                        break;
                    case 5:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                }
            } while (opcion != 5);
        }
    }
}