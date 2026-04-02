using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramasFinalV1.Programas
{
    internal class Matrices
    {
        // Creacion
        // Insercion
        // Recorrido
        // Busqueda
        // Ordenacion
        // Borrado
        internal Matrices() { }

        internal int?[,] CreacionMatrizRnd(int rows, int cols)
        {
            int?[,] matrix = new int?[rows, cols];
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = random.Next(1, 1000);

                }
            }
            return matrix;
        }

        internal int?[,] CreacionMatrizManual(int rows, int cols)
        {
            int?[,] matrix = new int?[rows, cols];
            Console.WriteLine($"\n--- Entrada Manual: {rows}x{cols} ---");
            Console.WriteLine($"Escriba los {cols} números separados por espacio y presione Enter.\n");

            for (int i = 0; i < rows; i++)
            {
                bool rowSuccess = false;
                while (!rowSuccess)
                {
                    Console.Write($"Fila {i + 1}/{rows}: ");
                    string input = Console.ReadLine() ?? "";

                    string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != cols)
                    {
                        Console.WriteLine($"Error: Se esperaban {cols} elementos, pero recibí {parts.Length}. Reintente.");
                        continue;
                    }

                    try
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            matrix[i, j] = int.Parse(parts[j]);
                        }
                        rowSuccess = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error: Uno de los elementos no es un número válido. Reintente.");
                    }
                }
            }
            Console.WriteLine("Matriz cargada con éxito.");
            return matrix;
        }


        internal void MostrarMatriz(int?[,] matrix)
        {
            for (int i = 0; matrix.GetLength(0) > i; i++)
            {
                for (int j = 0; matrix.GetLength(1) > j; j++)
                {
                    Console.Write($"[{matrix[i, j]}] ");
                }
                Console.Write("\n");
            }
        }
        internal void EncontrarElemento(int target, int?[,] matrix)
        {
            bool found = false;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == target)
                    {
                        Console.WriteLine($"Objeto: {target} encontrado en el indice: [{i + 1}, {j + 1}]");
                        found = true;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($"El numero {target} no existe en la matriz actual.");
            }
        }


        internal void BorrarElemento(int target, int?[,] matrix)
        {
            bool foundAtLeastOne = false;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == target)
                    {
                        matrix[i, j] = null;
                        foundAtLeastOne = true;

                        Console.WriteLine($"Objeto: {target} fue borrado en el indice: [{i + 1}, {j + 1}]");
                    }
                }
            }

            if (!foundAtLeastOne)
            {
                Console.WriteLine($"El numero {target} no se encuentra en la matriz.");
            }
        }

        public static double[,] Invert(double[,] matrix)
        {
            int n = matrix.GetLength(0);

            // 1. VALIDATION: only square matrices have inverses.
            if (n != matrix.GetLength(1))
                throw new ArgumentException("Matrix must be square.");

            // 2. AUGMENTATION: create a matrix of size n, 2n (twice as long).
            // It looks like: my matrix | identity matrix
            // 
            double[,] augmented = new double[n, 2 * n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    augmented[i, j] = matrix[i, j];

                // Place '1' in the diagonal of the right half (Identity)
                augmented[i, i + n] = 1.0;
            }

            // 3. GAUSS-JORDAN ELIMINATION
            for (int i = 0; i < n; i++)
            {
                // --- A: Partial Pivoting ---
                // find the row with the largest absolute value in the current column 'i'.
                // this reduces rounding errors and prevents dividing by zero.
                int pivotIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(augmented[j, i]) > Math.Abs(augmented[pivotIndex, i]))
                        pivotIndex = j;
                }

                // in case the pivotIndex isnt equal to i, we can assume theres another row with a larger value so we simply swap the rows with the code below
                if (pivotIndex != i)
                {
                    for (int k = 0; k < 2 * n; k++)
                    {
                        double temp = augmented[i, k];
                        augmented[i, k] = augmented[pivotIndex, k];
                        augmented[pivotIndex, k] = temp;
                    }
                }

                // --- STEP B: Singular Matrix Check ---
                // If the pivot is zero, the matrix is "singular" and has no inverse.
                if (Math.Abs(augmented[i, i]) < 1e-12)
                    throw new InvalidOperationException("Matrix is singular (cannot be inverted).");

                // --- C: Normalization ---
                // divide the entire pivot row by the pivot in order to get 1 in that specific row
                double divisor = augmented[i, i]; //here the divisor is always the diagonal value in each row i,i ie. 1,1 or 2,2
                for (int j = i; j < 2 * n; j++)
                    augmented[i, j] /= divisor;

                // --- D: elimination ---
                // now we are traversing the rows in the i column and check the values above or below the pivot to transform them into 0s and only those values.
                // we are transforming non pivot values that are right above or below the pivot from left to right using the most upper loop that holds i
                // we are doing this using this part of the code augmented[row, col] -= factor * augmented[i, col]; that will guarantee that the value above or
                // below the pivot will end up as 0, however the entire factor row needs to be subtracted as well in order to maintain the equality
                for (int row = 0; row < n; row++)
                {
                    if (row != i) // we are not subtracting the pivot row from itself
                    {
                        // Find the factor that would make the value in the current column 0.
                        double factor = augmented[row, i];

                        // Subtract (factor * pivotRow) from the current row.
                        for (int col = i; col < 2 * n; col++)
                        {
                            augmented[row, col] -= factor * augmented[i, col];
                        }
                    }
                }
            }

            // 4. EXTRACTION: The left side is now the Identity matrix.
            // The right side [n to 2n] is now the Inverse.
            double[,] inverse = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    inverse[i, j] = augmented[i, j + n];
            }

            return inverse;
        }

        public static double[] Solve(double[,] inverseMatrix, double[] b)
        {
            int n = b.Length;
            double[] x = new double[n];

            for (int i = 0; i < n; i++)
            {
                x[i] = 0;
                for (int j = 0; j < n; j++)
                {
                    // multiplies the inverse by the constants
                    x[i] += inverseMatrix[i, j] * b[j];
                }
            }
            return x;
        }



        /*
         So basically the logic goes like this.. i first pass into the function the matrix and then the fuction
         starts off by checking that the dimensions of the matrix are right.. (it must be 9x9), if ok the matrix 
         proceeds by checking every single cell horizontally, verticaly and each submatrix.. 
         this is done by nesting 1 for inside another each with the condition n < 9.. since the first loop will
         be used to check both rows and cols at the same time with the help of the second for as well.. by only swapping the
         i and j when needed.. and then we proceed with the most difficult part of the program that 
         is the checking of the 3x3 subarrays. 
         */

        /*
         Remdinder that the checking is done by using 
         */


        public static bool EsSolucionValida(int?[,] matrix)
        {
            if (matrix.GetLength(0) != 9 || matrix.GetLength(1) != 9)
                return false;

            for (int i = 0; i < 9; i++)
            {
                // Checklists for numbers 1-9 for this iteration
                bool[] rowCheck = new bool[9];
                bool[] colCheck = new bool[9];
                bool[] gridCheck = new bool[9];

                for (int j = 0; j < 9; j++)
                {
                    // 1. checks row
                    if (!ValidarCelda(matrix[i, j], rowCheck)) return false;

                    // 2. checks column
                    if (!ValidarCelda(matrix[j, i], colCheck)) return false;

                    // 3. checks 3x3 submatrix

                    /*
                     1. Row Offset [3 * (i / 3)]: 
 * Determines the vertical starting point of the 3x3 box.
 * i = 0,1,2 (Top)    -> 0/3, 1/3, 2/3 = 0 -> Offset 0
 * i = 3,4,5 (Middle) -> 3/3, 4/3, 5/3 = 1 -> Offset 3
 * i = 6,7,8 (Bottom) -> 6/3, 7/3, 8/3 = 2 -> Offset 6
 *
 * 2. Column Offset [3 * (i % 3)]: 
 * Determines the horizontal starting point of the 3x3 box.
 * i = 0,3,6 (Left)   -> 0%3, 3%3, 6%3 = 0 -> Offset 0
 * i = 1,4,7 (Center) -> 1%3, 4%3, 7%3 = 1 -> Offset 3
 * i = 2,5,8 (Right)  -> 2%3, 5%3, 8%3 = 2 -> Offset 6
 * 
 * 
 * 
 * so basically the submatrix traversal goes like this x -> x -> x
 *                                                     x -> x -> x
 *                                                     x -> ......
 *                                                     this happens on both inside and outside the submatrixes
 * basically from left to right and from up to down
                     */

                    int rowIdx = 3 * (i / 3) + (j / 3);
                    int colIdx = 3 * (i % 3) + (j % 3);
                    if (!ValidarCelda(matrix[rowIdx, colIdx], gridCheck)) return false;
                }
            }
            return true;
        }

        private static bool ValidarCelda(int? valor, bool[] checklist)
        {
            // the cell cant be null
            if (valor == null) return false;

            int num = valor.Value;

            // verifies numbers are between 1 and 9
            if (num < 1 || num > 9) return false;

            // verifies the num is not in the checklist already
            if (checklist[num - 1]) return false;

            // marks as seen
            checklist[num - 1] = true;
            return true;
        }

        public static bool EsSolucionValida(int[,] matrix)
        {
            if (matrix.GetLength(0) != 9 || matrix.GetLength(1) != 9)
                return false;

            for (int i = 0; i < 9; i++)
            {
                bool[] rowCheck = new bool[9];
                bool[] colCheck = new bool[9];

                for (int j = 0; j < 9; j++)
                {
                    if (!ValidarCelda(matrix[i, j], rowCheck)) return false;
                    if (!ValidarCelda(matrix[j, i], colCheck)) return false;
                }
            }
            return true;
        }

        private static bool ValidarCelda(int valor, bool[] checklist)
        {
            if (valor < 1 || valor > 9) return false;
            if (checklist[valor - 1]) return false;
            checklist[valor - 1] = true;
            return true;
        }
    }
}
