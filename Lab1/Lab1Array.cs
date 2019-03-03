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
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    static class Lab1Array
    {
        /// <summary>
        /// Вывод массива в формате [ a0, a1, a2, a3 ]
        /// </summary>
        /// <param name="ar">Массив данных</param>
        public static void Print(int[] ar)
        {
            Console.Write("[ ");
            foreach (int x in ar)
                Console.Write($"{x:D}; ");
            Console.WriteLine("]");
        }

        /// <summary>
        /// Чтение массива с консоли.
        /// </summary>
        /// <param name="ar">Выходной массив данных</param>
        public static void Read(out int[] ar)
        {
            Console.WriteLine("Введите длину массива:");
            uint len = uint.Parse(Console.ReadLine());
            ar = new int[len];

            Console.WriteLine("Введите {0} элементов:", len);
            int i = 0;
            while (i < len)
            {
                foreach (string value in Console.ReadLine().Split(' '))
                {
                    // при наличии лишних пробелов будут пустые строки
                    if (value.Length == 0)
                        continue;
                    // если значений в строке слишком много, то пропускаем лишние
                    if (i >= len)
                        break;

                    ar[i] = int.Parse(value);
                    i += 1;
                }
            }
        }

        /// <summary>
        /// Нахождение номера первого минимального элемента, начиная с элемента с номером start.
        /// </summary>
        /// <returns>Индекс первого минимального элемента в массиве после start</returns>
        /// <param name="ar">Массив данных</param>
        /// <param name="start">Смещение в массиве, по умолчанию - 0</param>
        public static void MinIndex(ref int[] ar, ref int firstMin, ref int secMin)
        {
            bool isFirstIndexFound = false;
            int index = 0;
            List<int> num = new List<int>();
            foreach (var item in ar)
            {
                num.Add(item);
            }
            for (int i = 0; i < num.Count; i++)
            {
                if (num[i] == num.Min() && !isFirstIndexFound)
                {
                    index = i;
                    firstMin = i;
                    isFirstIndexFound = true;
                }
            }
            num.RemoveAt(index);
            for (int i = 0; i < num.Count; i++)
            {
                if (num[i] == num.Min() && isFirstIndexFound)
                {
                    secMin = i + 1;
                }
            }
        }

        /// <summary>
        /// Обращение порядка элементов массива между двумя минимальными элементами
        /// </summary>
        /// <param name="ar">Массив данных</param>
        public static void ReverseBetweenTwoMins(ref int[] ar)
        {
            int firstMin = 0, secondMin = 0;
            MinIndex(ref ar, ref firstMin, ref secondMin);
            Array.Reverse(ar, firstMin + 1, secondMin - 1);
        }

        /// <summary>
        /// Отсортировать элементы массива таким образом, 
        /// чтобы сначала шли все положительные элементы, а затем все отрицательные.
        /// Исходный порядок следования элементов не должен быть нарушен.
        /// </summary>
        /// <param name="ar">Массив данных</param>
        public static void SortPositive(ref int[] ar)
        {
            int[] pos = new int[0];
            int[] neg = new int[0];
            foreach (var i in ar)
            {
                if (i >= 0)
                {
                    Array.Resize(ref pos, pos.Length + 1);
                    pos[pos.Length - 1] = i;
                }
                else
                {
                    Array.Resize(ref neg, neg.Length + 1);
                    neg[neg.Length - 1] = i;
                }
            }
            for (int i = 0; i < neg.Length; i++)
            {
                Array.Resize(ref pos, pos.Length + 1);
                pos[pos.Length - 1] = (neg[i]);
            }
            ar = pos;
        }

        /// <summary>
        /// Вставить дубликаты значений после каждого элемента в массиве.
        /// </summary>
        /// <param name="ar">Массив данных</param>
        public static void InsertDuplicates(ref int[] ar)
        {
            List<int> arr = new List<int>();
            for (int i = 0; i < ar.Length; i++)
            {
                arr.Add(ar[i]);
                arr.Add(ar[i]);
            }
            ar = arr.ToArray();
        }

        /// <summary>
        /// Удалить повторяющиеся значения из массива, не нарушая порядок его элементов.
        /// </summary>
        /// <param name="ar">Массив данных</param>
        public static void RemoveDuplicates(ref int[] ar)
        {
            for (int i = 0; i < ar.Length - 1; i++)
            {
                for (int j = i + 1; j < ar.Length; j++)
                {
                    int dupId;
                    if (ar[i] == ar[j])
                    {
                        dupId = j;
                        Swap(ref ar[dupId], ref ar[ar.Length - 1]);
                        Array.Resize(ref ar, ar.Length - 1);
                    }

                }
            }
        }

        private static void Swap(ref int num1, ref int num2)
        {
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }
    }
}
