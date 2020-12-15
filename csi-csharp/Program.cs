using System;
using System.Collections.Generic;

namespace csi_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var m = new MapBuilder();
            IEnumerable<string> lines = m.GetMap("C:\\dev\\advent\\csi-north-pole\\example.txt");
            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }

            lines = m.GetMap("C:\\dev\\advent\\csi-north-pole\\real-quiz.txt");
            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
