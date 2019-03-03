/*
 * Lab1
 * Отладка. Обработка массивов.
 * 
 * Часть1. Используя методы отладки по шагам найти ошибки в программе.
 * https://docs.microsoft.com/ru-ru/visualstudio/debugger/debugger-feature-tour?view=vs-2017
 * 
 * Часть2. Реализовать закоментированные методы из класса Lab1Array
 */
using System;

namespace Lab1
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] a;
            Lab1Array.Read(out a);
            Lab1Array.Print(a);
            // TODO: Найдите ошибку в программе:
            Lab1Array.ReverseBetweenTwoMins(ref a);
            Lab1Array.Print(a);
            Lab1Array.SortPositive(ref a);
            Lab1Array.Print(a);
            Lab1Array.InsertDuplicates(ref a);
            Lab1Array.Print(a);
            Lab1Array.RemoveDuplicates(ref a);
            Lab1Array.Print(a);
            Console.ReadKey();
        }
    }
}
