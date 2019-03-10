using System;
using System.IO;

namespace Lab5
{
    public enum Gender { Male, Female, Other }

    public class Person
    {
        protected string firstname;
        protected string lastname;
        protected string patronim;
        protected Gender gender;

        public Person(Gender gender, string firstname, string lastname, string patronim = "")
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.patronim = patronim;
            this.gender = gender;
        }

        public static Person Read(TextReader input)
        {
            string[] components = input.ReadLine().Split(";");

            string[] nameComponents = components[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string firstname;
            string lastname;
            string patronim;

            if (nameComponents.Length == 3) // с отчеством
            {
                firstname = nameComponents[1];
                lastname = nameComponents[0];
                patronim = nameComponents[2];
            }
            else if (nameComponents.Length == 2) // без отчества
            {
                firstname = nameComponents[1];
                lastname = nameComponents[0];
                patronim = "";
            }
            else
                throw new FormatException("Unaxpected Name Format");

            string gender = components[1].Trim();
            Gender g = Gender.Other;

            switch (gender[0])
            {
                case 'М':
                case 'м':
                case 'M': 
                case 'm':
                    g = Gender.Male;
                    break;
                case 'Ж':
                case 'ж':
                case 'F': 
                case 'f':
                    g = Gender.Female;
                    break;
            }

            return new Person(g, firstname, lastname, patronim);
        }

        public string Firstname
        {
            get { return this.firstname; }
        }

        public string Lastname
        {
            get { return this.lastname; }
        }

        public string Patronim
        {
            get { return this.patronim; }
        }

        public Gender Gender
        {
            get { return this.gender; }
        }

        public string FullName
        {
            get
            {
                return String.Join(" ", this.lastname, this.firstname, this.patronim).Trim();
            }
        }

        public override string ToString()
        {
            return $"{FullName}; {gender}";
        }
    }
}