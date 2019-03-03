/*
 * Lab4 Коллекции. Ассоциативные массивы
 * 
 * Часть 1. Дан CSV файл с данными журнала по предмету в формате:
 *     Иванов Иван Иванович; ; н; н; 2; н; н; н; н; 3
 *     Петров Петр Петрович; ; 5; ; 4; 4; ; 5; 5; 5
 * Считать информацию о посещениях и оценках в словарь студентов.
 * Выдавать информацию об ошибках формата файла.
 * 
 * Часть 2. По данным полученным из файла, получить:
 *     1. Список не аттестоваций студентов;
 *     2. Список результатов аттестованных студентов;
 *     4. Записать результаты всех студентов в новый файл в формате:
 *         Иванов Иван Иванович; н/а
 *         Петров Петр Петрович; 5
 */

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Lab4
{
    static class Lab4Collections
    {
        /// <summary>
        /// Генерация данных журнала
        /// </summary>
        /// <param name="students"> Путь к списку студентов</param>
        /// <param name="lessonsCount"> Количество занятий</param>
        public static void RandomJournal(string students, int lessonsCount)
        {
            StreamReader sr = new StreamReader(students);
            string journal = Path.GetFileNameWithoutExtension(students) + "Journal.csv";
            StreamWriter sw = new StreamWriter(journal);
            Random rnd = new Random();
            while (!sr.EndOfStream)
            {
                string studentName = sr.ReadLine();

                sw.Write(studentName);
                for (int i = 0; i < lessonsCount; ++i)
                {
                    int attest = rnd.Next(6);

                    sw.Write("; {0}", attest == 0 ? "-1" :
                                      attest == 1 ? "0" :
                                      attest.ToString());
                }
                sw.WriteLine();

            }
            sr.Close();
            sw.Close();
        }

        /// <summary>
        /// Считывание данных студентов из CSV файла.
        /// </summary>
        /// <returns> Данные о посещениях студентов</returns>
        /// <param name="journal"> Путь к файлу</param>
        public static Dictionary<string, List<int>> ReadStudentsData(string journal)
        {
            var jour = new Dictionary<string, List<int>>();
            using (var sr = new StreamReader("32Journal.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var chelik = sr.ReadLine().Split(";");
                    List<int> marks = new List<int>();
                    for (int i = 1; i < chelik.Length; i++)
                    {
                        marks.Add(Convert.ToInt32(chelik[i]));
                    }
                    jour.Add(chelik[0], marks);
                }
            }
            return jour;
        }

        /// <summary>
        /// Построение списка не аттестованных студентов на основе данных о посещениях.
        /// </summary>
        /// <returns> Список не аттестованных студентов</returns>
        /// <param name="studentsData"> Данные о посещениях студентов</param>
        public static List<string> FailedStudents(Dictionary<string, List<int>> studentsData)
        {
            var notAttested = new List<string>();
            foreach(var i in studentsData.Keys)
            {
                double aver = 0;
                var marks = studentsData[i];
                int lectures = marks.Count;
                for (int j = 0; j < marks.Count; j++)
                {
                    aver += marks[j];
                    if (marks[j] == 0 || marks[j] == -1)
                    {
                        lectures--;
                    }
                }
                if (aver / lectures < 3)
                {
                    notAttested.Add(i);
                }
            }
            return notAttested;
        }

        /// <summary>
        /// Построение списка оценок студентов получивших аттестацию.
        /// </summary>
        /// <returns> Оценки аттестованых студентов за курс</returns>
        /// <param name="studentsData"> Данные об оценках студентов</param>
        public static Dictionary<string, int> Results(Dictionary<string, List<int>> studentsData)
        {
            var attested = new Dictionary<string, int>();
            foreach(var i in studentsData.Keys)
            {
                double aver = 0;
                var marks = studentsData[i];
                int lectures = marks.Count;
                for (int j = 0; j < marks.Count; j++)
                {
                    aver += marks[j];
                    if (marks[j] == 0 || marks[j] == -1)
                    {
                        lectures--;
                    }
                }
                if (aver / lectures > 3)
                {
                    attested.Add(i, (int)aver / lectures);
                }
            }
            return attested;
        }

        /// <summary>
        /// Вывод данных об аттестации студентов в новый файл.
        /// </summary>
        /// <param name="studentsData"> Данные об оценках студентов</param>
        /// <param name="output"> Путь к выходному файлу</param>
        public static void ResultsFile(Dictionary<string, int> attested, List<string> notAttested, string output)
        {
            using (var sr = new StreamWriter(output))
            {
                foreach(var i in attested)
                {
                    sr.WriteLine($"{i.Key} с оценкой {i.Value} \t\t аттестован");
                }
                for (int i = 0; i < notAttested.Count; i++)
                {
                    sr.WriteLine($"{notAttested[i]} \t\t не аттестован");
                }
            }
        }
    }
}

