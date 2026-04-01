using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramasFinalV1.Programas
{
    internal class Vectores
    {

        // Creacion
        // Insercion
        // Recorrido
        // Busqueda
        // Ordenacion
        // Borrado
        public Vectores() { }
        public int?[] CrearVectorRnd(int limit)
        {
            Console.WriteLine("\nArray creado:");
            int?[] arr = new int?[limit];
            Random rnd = new Random();
            for (int i = 0; i < limit; i++)
            {
                arr[i] = rnd.Next(1, 100);
                Console.Write($"[{arr[i]}] ");
            }
            Console.WriteLine();
            return arr;
        }

        public int?[] CrearVectorManual()
        {
            Console.WriteLine("Entrada manual");
            Console.WriteLine("Escriba los números separados por espacio (Ej: 1 2 3) and press Enter.\n");

            while (true)
            {
                Console.Write("Elementos: ");
                string input = Console.ReadLine() ?? "";

                string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int?[] res = new int?[parts.Length];
                bool allValid = true;

                for (int i = 0; i < parts.Length; i++)
                {
                    if (int.TryParse(parts[i], out int numRes))
                    {
                        res[i] = numRes;
                    }
                    else
                    {
                        Console.WriteLine($"[!] Error: '{parts[i]}' is not a valid number.");
                        allValid = false;
                        break;
                    }
                }

                if (allValid && res.Length > 0)
                {
                    Console.WriteLine("Array cargado exitosamente!");
                    return res;
                }

                if (res.Length == 0) Console.WriteLine("[!] No se ingresaron números.");
            }






            /*
            int?[] arr = new int?[limit];
            Console.WriteLine($"\n--- Entrada Manual: {limit} elementos ---");
            Console.WriteLine("Escriba los números separados por espacio y presione Enter.\n");

            bool success = false;
            while (!success)
            {
                Console.Write("Elementos: ");
                string input = Console.ReadLine() ?? "";

                string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != limit)
                {
                    Console.WriteLine($"  [!] Error: Se esperaban {limit} elementos, pero recibí {parts.Length}. Reintente.");
                    Console.WriteLine($"  Acuerdese de escrbir los numeros separados por un espacio!");
                    Console.WriteLine("Por ejemplo: \n1 2 3 4 5 etc");
                    continue;
                }

                try
                {
                    for (int j = 0; j < limit; j++)
                    {
                        arr[j] = int.Parse(parts[j]);
                    }
                    success = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Uno de los elementos no es un número válido. Reintente.");
                }
            }
            Console.WriteLine("✔ Array cargado con éxito.");
            return arr;
            */
        }

        public void RecorridoVector(int?[] arr)
        {
            if (arr == null)
            {
                Console.WriteLine("Array vacio.");
                return;
            }
            Console.WriteLine("\nContenido del Array:");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"[{arr[i]}] ");
            }
        }

        public void BusquedaVector(int?[] arr, int target)
        {
            int cantidad = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == target)
                {
                    cantidad++;
                }
                if (cantidad == 1)
                {
                    Console.WriteLine($"Su elemento '{target}' se encontro en el indice: {i}");
                    return;
                }
                Console.WriteLine($"Su elemento '{target}' se encontro en el indice: {cantidad}");

            }
            Console.WriteLine("No se encontro su elemento");
        }

        public int?[] EliminarElemento(int?[] arr, int target)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == target)
                {
                    arr[i] = null;
                }
            }
            int?[] res = RecorrerVector(arr);
            return res;
        }

        public int?[] RecorrerVector(int?[] arr)
        {
            int?[] res = new int?[arr.Length];
            int j = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null)
                {
                    res[j] = arr[i];
                    j++;
                }

            }

            return res;
        }




        public int?[] BubbleSort(int?[] arr)
        {
            bool swapped = true;
            while (swapped)
            {
                int passes = 0;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int temp = (int)arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        passes++;
                    }
                }
                if (passes == 0) swapped = false;
            }
            Console.WriteLine("\nArray ordenado con BubbleSort.");
            return arr;
        }

        public int?[] ShellSort(int?[] arr)
        {
            int n = arr.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int? temp = arr[i];
                    int j;
                    for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                    {
                        arr[j] = arr[j - gap];
                    }
                    arr[j] = temp;
                }
            }
            Console.WriteLine("\nArray ordenado con ShellSort.");
            return arr;
        }

        public int?[] QuickSort(int?[] arr)
        {
            if (arr.Length == 0)
            {
                Console.WriteLine("\n✔ Array ordenado con QuickSort.");
                return arr;
            }
            QuickSortHelper(arr, 0, arr.Length - 1);
            Console.WriteLine("\n✔ Array ordenado con QuickSort.");
            return arr;
        }

        private void QuickSortHelper(int?[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = PartitionQuick(arr, left, right);
                QuickSortHelper(arr, left, pivotIndex - 1);
                QuickSortHelper(arr, pivotIndex + 1, right);
            }
        }

        private int PartitionQuick(int?[] arr, int left, int right)
        {
            int? pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    int? temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            int? temp2 = arr[i + 1];
            arr[i + 1] = arr[right];
            arr[right] = temp2;

            return i + 1;
        }

        public bool SonAnagramas(string palabra1, string palabra2)
        {
            if (palabra1.Length != palabra2.Length)
            {
                return false;
            }

            int[] abc = new int[256];
            for (int i = 0; i < palabra1.Length; i++)
            {
                abc[palabra1[i]]++;
                abc[palabra2[i]]--;
            }

            for (int i = 0; i < abc.Length; i++)
            {
                if (abc[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
