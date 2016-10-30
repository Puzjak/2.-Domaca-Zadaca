using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad3
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }

        public Student(Student student)
        {
            Name = student.Name;
            Jmbag = student.Jmbag;
            Gender = student.Gender;
        }

        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public Student(string name, string jmbag, Gender gender)
        {
            Name = name;
            Jmbag = jmbag;
            Gender = gender;

        }

        public override bool Equals(object s)
        {
            var item = s as Student;
            if (object.ReferenceEquals(this, null) || object.ReferenceEquals(s, null))
                return false;
            return this.Jmbag == item.Jmbag;
        }


        public override int GetHashCode()
        {
            return this.Jmbag.GetHashCode();
        }

        public static bool operator ==(Student s1, Student s2)
        {
            if (object.ReferenceEquals(s1, null) || object.ReferenceEquals(s2, null))
                return false;
            return s1.Jmbag == s2.Jmbag;
        }

        public static bool operator !=(Student s1, Student s2)
        {
            if (object.ReferenceEquals(s1, null) || object.ReferenceEquals(s2, null))
                return false;
            return s1.Jmbag != s2.Jmbag;
        }

    }
}
