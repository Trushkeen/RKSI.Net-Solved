using System;
using System.IO;

namespace Lab2
{
    static class Matrix
    {
        /// <summary>
        /// Вывод марицы на консоль
        /// </summary>
        /// <param name="matr">Исходная матрица</param>
        public static void Print(double[,] matr)
        {
            for (int i = 0; i < matr.GetLength(0); ++i)
            {
                Console.Write("[ ");

                for (int j = 0; j < matr.GetLength(1); ++j)
                    Console.Write("{0,5:F2} ", matr[i, j]);

                Console.WriteLine("]");
            }
        }

        /// <summary>
        /// Чтение матрицы из текстового файла в формате:
        ///   3 4
        ///   1.0 2.0 3.0 4.0
        ///   4.0 5.0 6.0 7.0
        ///   7.0 8.0 9.0 10.0
        /// </summary>
        /// <returns>Считанная матрица</returns>
        /// <param name="source">Поток данных из файла</param>
        public static double[,] Read()
        {
            using (var sr = new StreamReader("matrix.txt"))
            {
                var colrow = sr.ReadLine().Split();
                int row = int.Parse(colrow[0]);
                int col = int.Parse(colrow[1]);

                double[,] matr = new double[row, col];
                while (!sr.EndOfStream)
                {
                    for (int i = 0; i < row; i++)
                    {
                        var line = sr.ReadLine().Split();
                        for (int j = 0; j < col; j++)
                        {
                            matr[i, j] = int.Parse(line[j]);
                        }
                    }
                }
                return matr;
            }
        }

        /// <summary>
        /// Запись матрицы в текстовый файл в формате:
        ///   3 4
        ///   1.0 2.0 3.0 4.0
        ///   4.0 5.0 6.0 7.0
        ///   7.0 8.0 9.0 10.0
        /// </summary>
        /// <param name="matr">Исходная матрица</param>
        /// <param name="file">Выходной файл</param>
        public static void Write(double[,] matr)
        {
            using (var sw = new StreamWriter("matrix_wroted.txt"))
            {
                sw.Write(matr.GetLength(0) + " ");
                sw.Write(matr.GetLength(1) + " ");
                sw.Write("\r\n");
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        sw.Write(matr[i, j] + " ");
                    }
                    sw.Write("\r\n");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The sum line.</returns>
        /// <param name="matr">Matr.</param>
        public static double MinSumRow(double[,] matr)
        {
            double sum = 0;
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                sum += matr[0, i];
            }
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                double newsum = 0;
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    newsum += matr[i, j];
                }
                if (newsum < sum) sum = newsum;
            }
            return sum;
        }


        /// <summary>
        /// Transpose the specified matr.
        /// </summary>
        /// <param name="matr">Matr.</param>
        public static double[,] Transpose(double[,] matr)
        {
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1) - 2; j++)
                {
                    Swap(ref matr[i, j], ref matr[j, i]);
                }
            }
            return matr;
        }

        private static void Swap(ref double a, ref double b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Сложение матриц
        /// </summary>
        /// <returns>Сумма матриц</returns>
        /// <param name="left">Левая матрица</param>
        /// <param name="right">Правая матрица</param>
        public static double[,] Sum(double[,] left, double[,] right)
        {
            if (left.GetLength(0) == left.GetLength(1) && left.GetLength(1) == right.GetLength(1))
            {
                double[,] summed = new double[left.GetLength(0), left.GetLength(1)];
                for (int i = 0; i < left.GetLength(0); i++)
                {
                    for (int j = 0; j < left.GetLength(1); j++)
                    {
                        summed[i, j] = left[i, j] + right[i, j];
                    }
                }
                return summed;
            }
            double[,] mar = new double[1, 1];
            return mar;
        }

        /// <summary>
        /// Умножение матриц
        /// </summary>
        /// <returns>Произведение матриц</returns>
        /// <param name="left">Левая матрица</param>
        /// <param name="right">Правая матрица</param>
        public static double[,] Multiply(double[,] left, double[,] right)
        {
            double[,] mul = new double[left.GetLength(0), right.GetLength(1)];
            if (left.GetLength(1) == right.GetLength(0))
            {
                for (int i = 0; i < left.GetLength(0); i++)
                {
                    for (int j = 0; j < right.GetLength(1); j++)
                    {
                        for (int k = 0; k < right.GetLength(0); k++)
                        {
                            mul[i, j] += left[i, k] * right[k, j];
                        }
                    }
                }
            }
            return mul;
        }

    }
}

