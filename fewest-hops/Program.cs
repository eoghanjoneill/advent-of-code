using System;
using System.IO;
using System.Linq;

namespace fewest_hops
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] examples = File.ReadAllLines("C:\\dev\\advent\\fewest-hops\\quiz.txt");
            int i = 1;
            foreach (string s in examples)
            {   
                if (s.Length> 0){
                    char c = s[0];
                    if (c >= 'A' && c <= 'Z') {
                        Console.WriteLine(c);
                    }
                }
                if (s.StartsWith('['))
                {
                    int[] numbers = s.Trim().Trim(new char[] {'[',']'}).Split(',').Select(token => {
                        return int.Parse(token.Trim());
                    }).ToArray();
                    var at = new ArrayTraverser(numbers);
                    int hops = at.GetMinHops();
                    Console.WriteLine($"Puzzle {i} answer: {hops}");
                    i++;
                }
                
            }
        }
    }
}
