using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad3
{
    public class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }

        public University(string name, Student[] students )
        {
            Name = name;
            Students = students;
        }

        public static University[] GetAllCroatianUniversities()
        {
            var studentiNaFeru = new Student[3]
            {
                new Student("Ivan", "12345", Gender.Male),
                new Student("Matija", "0036492904", Gender.Male),
                new Student("Dorotea", "12346", Gender.Female),
            };

            var studentiNaMedicini = new Student[3]
            {
                new Student("Lucija", "22345", Gender.Female), 
                new Student("Irma", "22346", Gender.Female), 
                new Student("Marko", "22347", Gender.Male)
            };
            var studentiNaFsbu = new Student[2]
            {
                new Student("Patrik", "32345", Gender.Male),
                new Student("Ana", "32346", Gender.Female)
            };
            var studentiNaEkonomiji = new Student[2]
            {
                new Student("Kristina", "42345", Gender.Female),
                new Student("Dorotea", "12346", Gender.Female)
            };
            var muskiFaks = new Student[2]
            {
                new Student("Muski1", "1", Gender.Male),
                new Student("Muski2", "2", Gender.Male)
            };
            var list = new University[5]
            {
                new University("FER", studentiNaFeru),
                new University("medicina", studentiNaMedicini),
                new University("FSB", studentiNaFsbu),
                new University("ekonomija", studentiNaEkonomiji),
                new University("Muski Faks", muskiFaks)
            };
            return list;
        }
    }
}
