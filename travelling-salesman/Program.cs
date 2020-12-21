using System;
using System.IO;

namespace travelling_salesman
{
    class Program
    {
        static void Main(string[] args)
        {
            SolveRoute("C:\\dev\\advent\\travelling-salesman\\example.txt");
            SolveRoute("C:\\dev\\advent\\travelling-salesman\\quiz.txt");
        }

        static void SolveRoute(string path)
        {
            string[] routes = File.ReadAllLines(path);
            Salesman traveller = new Salesman(routes);
            int d = traveller.Solve();
            Console.WriteLine($"Shortest distance: {d}");
            
            foreach (string s in traveller.ShortestRouteCities)
            {
                Console.Write($"{s}>");
                
            }
            Console.WriteLine();
        }
    }
}
