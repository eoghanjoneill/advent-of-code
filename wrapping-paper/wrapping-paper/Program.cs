using System;
using System.Collections.Generic;
using System.IO;

namespace wrapping_paper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IEnumerable<string> lines = File.ReadLines("C:\\dev\\advent\\wrapping-paper\\PaperCalaulatorTest\\example.txt");
            var c = new PaperCalculator();
            int sf = c.GetTotalSquareFeet(lines);
            Console.WriteLine($"Example: total sqft = {sf.ToString()}");

            lines = File.ReadLines("C:\\dev\\advent\\wrapping-paper\\PaperCalaulatorTest\\presents.txt");
            sf = c.GetTotalSquareFeet(lines);
            Console.WriteLine($"The real quiz: total sqft = {sf.ToString()}");

        }
    }
}
