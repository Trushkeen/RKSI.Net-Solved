using System;
using System.IO;

namespace Lab3
{
    class Lab3Files
    {
        /// <summary>
        /// Получает массив всех файлов в указанной директории.
        /// </summary>
        /// <returns> Данные файлов</returns>
        /// <param name="dir"> Директория поиска файлов</param>
        /// <param name="ext"> Маска расширения для файлов</param>
        /// <param name="rec"> Флаг рекурсивного поиска файлов</param>
        public static FileInfo[] GetFiles(DirectoryInfo dir, string ext, bool rec = false)
        {
            if (!dir.Exists)
            {
                throw new ArgumentException(dir.FullName + " не существует");
            }
            var files = new FileInfo[0];
            if (!rec)
            {
                files = dir.GetFiles("*" + ext);
            }
            else
            {
                try
                {
                    files = dir.GetFiles("*" + ext, SearchOption.AllDirectories);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Было отказано в доступе");
                }
            }
            return files;
        }

        /// <summary>
        /// Единицы измерения памяти
        /// </summary>
        public enum MemUnits { Bytes = 0, KBytes = 1, MBytes = 2, GBytes = 3 }

        /// <summary>
        /// Получает количество единиц памяти в файле
        /// </summary>
        /// <returns> Количество единиц измерения в файле</returns>
        /// <param name="file"> Данные файла</param>
        /// <param name="units"> Единицы измерения памяти</param>
        public static uint GetMemLength(FileInfo file, out MemUnits units)
        {
            units = 0;
            uint size = (uint)file.Length;
            for (int i = 0; i < 4; i++)
            {
                if (size < 1024) break;
                size /= 1024;
                units++;
            }
            return size;
        }

        /// <summary>
        /// Выводит данные файла в заданный поток вывода
        /// </summary>
        /// <param name="output"> Поток вывода</param>
        /// <param name="file"> Файл данные которого нужно вывести</param>
        public static void PrintFileData(TextWriter output, FileInfo file)
        {
            using (var op = output)
            {
                op.WriteLine($"{file.Name}\t{file.CreationTime}\t{file.Length}");
            }
        }

    }
}
