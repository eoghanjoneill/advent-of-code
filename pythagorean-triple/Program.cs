using System;
using System.Collections.Generic;

namespace pythagorean_triple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var bdays = GetPythagoreanTriples(new DateTime(2001, 1, 1));
            Console.WriteLine($"Birthday count: {bdays.Count}, and they are:");
            foreach (var d in bdays)
            {
                Console.WriteLine(d.ToString("dd-MM-yy"));
            }
        }

        static List<DateTime> GetPythagoreanTriples(DateTime startDate)
        {            
            int centuryEnd = startDate.Year + 100;
            DateTime dateToCheck = startDate;
            List<DateTime> birthdays = new List<DateTime>();
            while (dateToCheck.Year < centuryEnd)
            {
                int day = dateToCheck.Day;
                int month = dateToCheck.Month;
                int year = int.Parse(dateToCheck.ToString("yy"));
                if (Math.Pow(day, 2) + Math.Pow(month, 2) == Math.Pow(year, 2))
                {
                    birthdays.Add(dateToCheck);
                }
                dateToCheck = dateToCheck.AddDays(1);                
            }
            return birthdays;
        }
    }
}
