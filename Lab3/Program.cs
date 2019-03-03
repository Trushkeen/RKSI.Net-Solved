/*
 * Lab3
 * Файлы.
 *
 * Часть1. Разрабтать консольное приложение для вывода данных о файлах.
 * В результате должны выводиться данные в виде:
 * <имя файла>\t<дата создания>\t<длина файла>
 */
using System;
using System.IO;

namespace Lab3
{
    class Program
    {
        public static void PrintHelp()
        {
            Console.WriteLine("-i \"path\": путь к анализируемому каталогу, обязательный параметр;");
            Console.WriteLine("-e \"ext\": расширение обрабатываемых файлов, необязательный параметр;");
            Console.WriteLine("-o \"path\": путь к выходному файлу, если задан этот ключ результат работы выводится в указанный файл, необязательный параметр;");
            Console.WriteLine("-r: флаг указывающий на рекурсивную обработку всех подкаталогов по указанного пути, необязательный параметр;");
            Console.WriteLine("-?: вывод этой информации.");
        }

        static void Main(string[] args)
        {
            string input = "", output = "", ext = "";
            bool rec = false;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i][0] == '-')
                {
                    switch (args[i][1])
                    {
                        case '?': PrintHelp(); break;
                        case 'i': input = args[i + 1]; break;
                        case 'e': ext = args[i + 1]; break;
                        case 'o': output = args[i + 1]; break;
                        case 'r': rec = true; break;
                        default: Console.WriteLine("No arguments"); break;
                    }
                }
            }
            TextWriter textWriter = new StreamWriter(output, true);
            var dir = new DirectoryInfo(input);
            var arr = Lab3Files.GetFiles(dir, ext);
            Lab3Files.MemUnits size;
            var b = Lab3Files.GetMemLength(arr[0], out size);
            Console.Write(b + " ");
            switch (size)
            {
                case 0: Console.WriteLine("Bytes"); break;
                case (Lab3Files.MemUnits)1: Console.WriteLine("KBytes"); break;
                case (Lab3Files.MemUnits)2: Console.WriteLine("MBytes"); break;
                case (Lab3Files.MemUnits)3: Console.WriteLine("GBytes"); break;
            }
            foreach (var i in arr)
            {
                Lab3Files.PrintFileData(textWriter, i);
            }
            Console.ReadKey();
        }
    }
}
