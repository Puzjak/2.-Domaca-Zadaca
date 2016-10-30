using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Zad3
{
    public class Program
    {
        public static void Main()
        {
            int[] integers = {1, 2, 2, 2, 3, 3, 4, 5, 2};

            IEnumerable<string[]> strings = integers.GroupBy(n => n).Where(n => n.Count() > 0)
                .Select(n => new String[]
                    {"Broj " + n.Key.ToString() + " se ponavlja " + n.Count() + " puta."});

            foreach (var element in strings)
            {
                foreach (var value in element)
                    Console.WriteLine(value);
            }

            Examples.Example1();
            Examples.Example2();
            Examples.Example3();

            University[] universities = University.GetAllCroatianUniversities();

            Student[] allCroatianStudents = universities.SelectMany(s => s.Students)
                .Distinct()
                .OrderBy(s => s.Jmbag)
                .ToArray();
            Student[] croatianStudentsOnMultipleUniversities = universities.SelectMany(s => s.Students)
                .GroupBy(s => s, s => s.Jmbag)
                .Where(s => s.Count() > 1)
                .Select(s => s.Key)
                .OrderBy(s => s)
                .ToArray();
            Student[] studentsOnMaleOnlyUniversities =
                universities.Where(s => s.Students.All(g => g.Gender == Gender.Male))
                    .SelectMany(s => s.Students)
                    .ToArray();

            Console.WriteLine("Croatian students: ");
            foreach(var element in allCroatianStudents)
                Console.WriteLine(element.Name + " - " + element.Jmbag);

            Console.WriteLine("\n\nCroatian students on multiple universities: ");
            foreach (var element in croatianStudentsOnMultipleUniversities)
                Console.WriteLine(element.Name + " - " + element.Jmbag);

            Console.WriteLine("\n\nCroatian students on male only universities: ");
            foreach (var element in studentsOnMaleOnlyUniversities)
                Console.WriteLine(element.Name + " - " + element.Jmbag);

            Console.ReadLine();
            }

        }
    }

