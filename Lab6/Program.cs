/*
 * Lab6
 * Интерфейсы. События. Представители(делегаты).
 * 
 * Часть 1. Реализовать класс с интерфейсом ICalculator. Использовать инкапсуляцию.
 *   Продемонстрировать работу класса на разборе заданной строки.
 * 
 * Часть 2. Реализовать метод, последовательно считывающий числа из файла, 
 *  и выводящий накопленную сумму на консоль.
 */
using System;

namespace Lab6
{
    class Program
    {
        static void Parse(ICalculator calc, string expression)
        {
            foreach(char c in expression)
            {
                if (char.IsDigit(c))
                {
                    calc?.AddDigit(c - '0');
                    Console.Write(c - '0');
                }
                else if (c == '=')
                {
                    Console.Write(c);
                    calc.Compute();
                    Console.WriteLine(calc.Result);
                }
                else
                {
                    object op = Enum.ToObject(typeof(CalculatorOperation), c);

                    if (Enum.IsDefined(typeof(CalculatorOperation), op))
                    {
                        Console.Write((char)(CalculatorOperation)op);
                        calc?.AddOperation((CalculatorOperation)op);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid character: " + c);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            try
            {
                var expr = Console.ReadLine();
                Parse(calc, expr);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Error: \"{e.Message}\"");
            }
            Console.ReadKey();
        }
    }
}