using System;
using System.Collections.Generic;
using System.IO;

namespace roman_logo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var rs = new RomanSorter();
            var lines = rs.CsvToSortedLines("C:\\dev\\advent\\roman-logo\\example.txt", true);
            foreach (string s in lines) {
                Console.WriteLine(s);
            }
            Console.WriteLine("");
            Console.WriteLine("*****************");
            Console.WriteLine("");

            lines = rs.CsvToSortedLines("C:\\dev\\advent\\roman-logo\\example.txt", false);
            foreach (string s in lines) {
                Console.WriteLine(s);
            }
            Console.WriteLine("");
            Console.WriteLine("*****************");
            Console.WriteLine("");

            lines = rs.CsvToSortedLines("C:\\dev\\advent\\roman-logo\\real-quiz.txt", true);
            foreach (string s in lines) {
                Console.WriteLine(s);
            }
            Console.WriteLine("");
            Console.WriteLine("*****************");
            Console.WriteLine("");

            lines = rs.CsvToSortedLines("C:\\dev\\advent\\roman-logo\\real-quiz.txt", false);
            foreach (string s in lines) {
                Console.WriteLine(s);
            }
        }
    }

    class RomanSorter
    {
        public IEnumerable<string> CsvToSortedLines(string path, bool includeLineNumber) {
            IEnumerable<string> lines = File.ReadLines(path);
            SortedDictionary<int,string> sd = new SortedDictionary<int, string>();
            foreach(string line in lines) {
                string[] tokens = line.Split(',');
                if (tokens.Length != 2) {
                    throw new Exception("comma issue");
                }
                sd.Add(RomanToArabic(tokens[0].Trim()), tokens[1].Trim());               
                
            }
            List<string> outLines = new List<string>();
            foreach (var pair in sd) {
                string s = "";
                if (includeLineNumber) {
                    s += pair.Key + "\t";
                }
                s += pair.Value;
                outLines.Add(s);
            }
            return outLines;
        } 
       

        private Dictionary<char, int> CharValues = null;
        private int RomanToArabic(string roman)
        {
            // Initialize the letter map.
            if (CharValues == null)
            {
                CharValues = new Dictionary<char, int>();
                CharValues.Add('I', 1);
                CharValues.Add('V', 5);
                CharValues.Add('X', 10);
                CharValues.Add('L', 50);
                CharValues.Add('C', 100);
                CharValues.Add('D', 500);
                CharValues.Add('M', 1000);
            }

            if (roman.Length == 0) return 0;
            roman = roman.ToUpper();

            // See if the number begins with (.
            if (roman[0] == '(')
            {
                // Find the closing parenthesis.
                int pos = roman.LastIndexOf(')');

                // Get the value inside the parentheses.
                string part1 = roman.Substring(1, pos - 1);
                string part2 = roman.Substring(pos + 1);
                return 1000 * RomanToArabic(part1) + RomanToArabic(part2);
            }

            // The number doesn't begin with (.
            // Convert the letters' values.
            int total = 0;
            int last_value = 0;
            for (int i = roman.Length - 1; i >= 0; i--)
            {
                int new_value = CharValues[roman[i]];

                // See if we should add or subtract.
                if (new_value < last_value)
                    total -= new_value;
                else
                {
                    total += new_value;
                    last_value = new_value;
                }
            }

            // Return the result.
            return total;
        }
    }
}
