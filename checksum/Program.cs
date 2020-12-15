using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace checksum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var filePath = args.Length == 0 ? "C:\\dev\\advent\\checksum\\input.csv" : args[0];
            Console.WriteLine(GetChecksum(filePath));
        }

        static int GetChecksum(string filePath) {
            List<int> line = new List<int>();
            int value;
            int total = 0;
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                while (csv.Read()) {
                    for(int i=0; csv.TryGetField<int>(i, out value); i++) {
                        line.Add(value);                    
                    }
                    int[] arr = line.ToArray();                               
                    line = new List<int>();
                    total += arr.Max() - arr.Min(); 
                }              
            }
            return total;
        }
    }
}
