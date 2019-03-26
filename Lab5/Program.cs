/*
 * Lab5 
 * Классы. Наследование
 * 
 * Часть 1. Реализовать наследника класса Person:
 *   - Сотрудник (Employee):
 *     - поле Отдел - название отдела, где работает сотрудник;
 *     - поле Должность - название должности, на которой работает сотрудник;
 *     - поле Зарплата - вещественное число;
 *     - метод ToString() - выводит должность, ФИО и отдел.
 *   Предусмотреть методы изменения данных сотрудника.
 * 
 * Часть 2. Заполнить массив Person[] случайными объектами (Person, Student, Employee). В этом массиве:
 *   - Найти сотрудника с минимальной зарплатой,
 *   - Вывести список студентов первого курса.
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5
{
    class Program
    {
        /// <summary>
        /// Gets the random person.
        /// </summary>
        /// <returns>The random person.</returns>
        /// <param name="names">Names.</param>
        /// <param name="uid">Uid.</param>
        static Person GetRandomPerson(string[] names, uint uid)
        {
            string name = names[new Random().Next(names.Length)];
            string surname = names[new Random().Next(names.Length)];
            var gender = Gender.Male;
            Person person = new Person(gender, name, surname + "ов");
            Random rnd = new Random();
            switch (rnd.Next(3))
            {
                case 0: return person;
                case 1: return new Student(person, "Sample", (uint)rnd.Next(3), (uint)rnd.Next(3));
                case 2: return new Employee(person, rnd.Next(9227, 50000));
            }
            return person;
        }

        static void Main(string[] args)
        {
            string[] names =
            { "Марат", "Богдан", "Сергей", "Андрей", "Илья", "Денчик", "Глеб", "Александр",
            "Герман", "Евгений" };
            Student stud = new Student("Иван", "Иванов", "Иванович", Gender.Male, "ПОКС", 1, 2);
            Person[] peoples = new Person[10];
            Dictionary<string, double> salaries = new Dictionary<string, double>();

            List<string> firstCourses = new List<string>();
            for (int i = 0; i < peoples.Length; i++)
            {
                peoples[i] = GetRandomPerson(names, (uint)new Random().Next());
                if (peoples[i] is Employee emp)
                {
                    //var emp = peoples[i] as Employee;
                    salaries.Add(emp.FullName, emp.Salary);
                }
                if (peoples[i] is Student)
                {
                    var st = peoples[i] as Student;
                    if (st.Course == 1) firstCourses.Add(st.FullName);
                }
            }

            // ToString() вызывается автоматически при преобразовании к строке
            Console.WriteLine(stud);
            // ToString() - виртуальная функция: будет позднее связывание
            Console.WriteLine(stud as Person);
            // FullName - не виртуальное свойство: будет раннее связывание
            Console.WriteLine(stud.FullName);
            Console.WriteLine((stud as Person).FullName);
            // Вызов статического метода
            //Person pers = Person.Read(Console.In);
            //Student stud2 = new Student(pers, "БТ", 3, 1);
            //Console.WriteLine(stud2);
            string minSalaryStr = "";
            double minSalaryDouble = salaries.Values.Min();
            foreach (var i in salaries)
            {
                if(i.Value == minSalaryDouble)
                {
                    minSalaryStr = i.Key;
                }
            }
            Console.WriteLine("Минимальная зарплата: " + minSalaryDouble + " y " + minSalaryStr);
            Console.WriteLine("Первокурсники:");
            foreach (var i in firstCourses)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }
    }
}
