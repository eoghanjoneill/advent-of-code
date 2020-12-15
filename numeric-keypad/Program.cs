using System;
using System.Collections.Generic;
using System.IO;

namespace numeric_keypad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Keypad k = new Keypad();
            var example = ReadFile("C:\\dev\\advent\\numeric-keypad\\code-example.txt");
            Console.WriteLine("Example code is " + k.GetCode(example));

            var question = ReadFile("C:\\dev\\advent\\numeric-keypad\\question.txt");
            Console.WriteLine("Question code is " + k.GetCode(question));

        }

        static string[] ReadFile(string path)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    lines.Add(sr.ReadLine().Trim());
                }
            }
            return lines.ToArray();
        }

    }
}
