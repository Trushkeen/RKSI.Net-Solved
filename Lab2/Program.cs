/*
 * Lab2
 * Матрицы. Работа с файлами.
 * 
 * Часть 1. Реализовать чтение и запись матрицы в файл.
 * 
 * Часть 2. Рализовать методы обработки матриц.
 */
using System;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] matr = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            Matrix.Print(matr);
            double[,] matr1 = Matrix.Read();
            Matrix.Print(matr1);
            Matrix.Write(matr);
            Console.WriteLine(Matrix.MinSumRow(matr));
            matr = Matrix.Transpose(matr);
            Matrix.Print(matr);
            Console.WriteLine();
            double[,] sum = Matrix.Sum(matr, matr1);
            Matrix.Print(sum);
            Console.WriteLine();
            double[,] mul = Matrix.Multiply(matr, matr1);
            Matrix.Print(mul);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
