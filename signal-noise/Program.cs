using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace signal_noise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var example = GetMostFrequentChars("C:\\dev\\advent\\signal-noise\\example.txt");
            Console.WriteLine($"Example answer: {example}");

            var quiz = GetMostFrequentChars("C:\\dev\\advent\\signal-noise\\real-quiz.txt");
            Console.WriteLine($"Real quiz answer: {quiz}");
        }

        static string GetMostFrequentChars(string path)
        {
            StringBuilder sb = new StringBuilder();
            string[] lines = File.ReadAllLines(path);
            var len = lines[0].Trim().Length;
            Dictionary<char, int>[] counters = new Dictionary<char, int>[len];
            for (int i = 0; i < len; i++)
            {
                counters[i] = new Dictionary<char, int>();
            }
            foreach (string l in lines) {
                string s = l.Trim();
                int pos = 0;
                foreach (char c in s)
                {
                    if (counters[pos].ContainsKey(c))
                    {
                        counters[pos][c] = counters[pos][c]+1;                        
                    }
                    else
                    {
                        counters[pos].Add(c,1);
                    }
                    pos++;
                }
            }
            
            for (int i = 0; i < len; i++)
            {
                var d = counters[i];
                char mostFreqChar = d.OrderByDescending(kv => kv.Value).First().Key;
                sb.Append(mostFreqChar);
            }
            return sb.ToString();
        }
    }
    
}
