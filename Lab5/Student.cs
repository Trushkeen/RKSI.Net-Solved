using System;
using System.IO;
using System.Text;

namespace Lab5
{
    /// <summary>
    /// Класс студент.
    /// </summary>
    public class Student : Person
    {
        protected uint course;
        protected uint group;
        protected string track;
        protected uint accessCardNum;

        public Student(string firstname, string lastname, string patronim, Gender gender,
                       string track, uint course, uint group) : base(gender, firstname, lastname, patronim)
        {
            this.track = track;
            this.course = course;
            this.group = group;
            // IAccessCardHolder
            this.accessCardNum = (uint)new Random().Next();
        }

        public Student(Person p, string track, uint course, uint group) :
            this(p.Firstname, p.Lastname, p.Patronim, p.Gender, track, course, group)
        { }

        /// <summary>
        /// Read the specified input.
        /// </summary>
        public new static Student Read(TextReader sr)
        {
            Student stud = new Student("", "", "", Gender.Other, "", 0, 0);
            string[] line = sr.ReadLine().Split();
            stud.firstname = line[0];
            stud.lastname = line[1];
            stud.patronim = line[2];
            switch (line[3].Trim())
            {
                case "М":
                case "м":
                case "M":
                case "m":
                    stud.gender = Gender.Male;
                    break;
                case "Ж":
                case "ж":
                case "F":
                case "f":
                    stud.gender = Gender.Female;
                    break;
            }
            stud.track = line[4];
            stud.course = Convert.ToUInt32(line[5]);
            stud.group = Convert.ToUInt32(line[6]);
            return stud;
        }

        /// <summary>
        /// Номер курса.
        /// </summary>
        public uint Course
        {
            get { return this.course; }
        }

        /// <summary>
        /// Номер группы.
        /// </summary>
        public uint Group
        {
            get { return this.group; }
        }

        /// <summary>
        /// Учебная программа.
        /// </summary>
        public string Track
        {
            get { return this.track; }
        }

        /// <summary>
        /// Название группы.
        /// </summary>
        public string GetGroupName()
        {
            // Пример работы со StringBuilder
            StringBuilder result = new StringBuilder();

            result.Append(track).Append("-").Append(course).Append(group);

            return result.ToString();
        }

        /// <summary>
        /// Полное имя студента.
        /// </summary>
        public new string FullName // закрывает метод базового класса Person
        {
            get
            {
                return this.lastname + " " + this.firstname;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Lab5.Student"/>.
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()}; {GetGroupName()}";
        }

    }
}