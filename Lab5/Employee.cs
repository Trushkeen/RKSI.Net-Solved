using System;
/* Часть 1. Реализовать наследника класса Person:
 *   - Сотрудник(Employee) :
 *     - поле Отдел(department) - название отдела, где работает сотрудник;
 *     - поле Должность(jobTitle) - название должности, на которой работает сотрудник;
 *     - поле Зарплата(salary) - вещественное число;
 *     - метод ToString() - выводит должность, ФИО и отдел.
 *   Предусмотреть методы изменения данных сотрудника.
 */

namespace Lab5
{
    public class Employee : Person
    {
        protected string department;
        protected string jobTitle;
        protected double salary;

        public Employee(Gender gender, string firstname, string lastname, string patronim = "",
            double salary = 0.0, string department = "", string jobTitle = "")
            : base(gender, firstname, lastname, patronim)
        {
            this.department = department;
            this.jobTitle = jobTitle;
            this.salary = salary;
        }

        public Employee(Person p, double salary = 0.0, string department = "", string jobTitle = "")
            : this(p.Gender, p.Firstname, p.Lastname, p.Patronim)
        {
            this.department = department;
            this.jobTitle = jobTitle;
            this.salary = salary;
        }

        public string Department
        {
            get
            {
                return this.department;
            }
        }

        public string JobTitle
        {
            get
            {
                return this.jobTitle;
            }
        }

        public double Salary
        {
            get
            {
                return this.salary;
            }
        }

        public override string ToString()
        {
            return $"{this.jobTitle}\n{base.firstname} {base.lastname} {base.patronim}\nОтдел: {this.department}";
        }
    }
}
