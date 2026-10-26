using System;

namespace ejercicio_14
{
    internal class Validador
    {
        public static int LeerEnteros(string mensaje)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor))
                {
                    if (valor >= 1 && valor <= 5)
                    {
                        return valor;
                    }
                    Console.WriteLine("Error: Opción fuera de rango (1-5)");
                }
                else
                {
                    Console.WriteLine("Error: Ingresa un valor numérico válido");
                }
            }
        }

        public static double LeerDecimales(string mensaje)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                if (double.TryParse(Console.ReadLine(), out valor))
                {
                    if (valor >= 0 && valor <= 20)
                    {
                        return valor;
                    }
                    Console.WriteLine("Error: Las notas deben estar entre 0 y 20");
                }
                else
                {
                    Console.WriteLine("Error: Ingresa un valor numérico válido");
                }
            }
        }

        public static string LeerTexto(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string valor = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(valor))
                {
                    return valor;
                }
                Console.WriteLine("Error: El campo no puede estar vacío");
            }
        }
    }
}