using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace taxi_grid
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Taxi t = GetTaxi("C:\\dev\\advent\\taxi-grid\\example.txt");
            Console.WriteLine($"Example taxi actual fare: {t.GetActualFare()} and best fare: {t.GetBestFare()}; X: {t.X}, Y: {t.Y}, Dir: {t.Direction}");
            t = GetTaxi("C:\\dev\\advent\\taxi-grid\\real-quiz.txt");
            Console.WriteLine($"Real quiz taxi actual fare: {t.GetActualFare()} and best fare: {t.GetBestFare()}; X: {t.X}, Y: {t.Y}, Dir: {t.Direction}");
        }

        static Taxi GetTaxi(string path)
        {
            string s = File.ReadLines(path).ToArray()[0];
            IEnumerable<string> instructions = s.Split(',').Select(i => i.Trim());
            var p = new Taxi();
            foreach (string instruction in instructions) {
                p.ProcessInstruction(instruction);
            }
            
            return p;
        }
    }    

    class Taxi
    {
        public Taxi() {
            this.X = 0;
            this.Y = 0;
            this.Direction = 0;
            kmsTravelled = 0;
        }
        public int X { get; set; }
        public int Y { get; set; }

        //N = 0, E = 1, S = 2, W = 3
        public int Direction { get; set; }
        public int kmsTravelled { get; set; }

        public void ProcessInstruction(string ins) {
            char lr = ins[0];
            int kms = int.Parse(ins.Substring(1));
            kmsTravelled += kms;
            if (lr == 'L')
            {
                if (Direction > 0)
                {
                    Direction--;
                }
                else
                {
                    Direction = 3;
                }
            }
            else
            {
                if (Direction < 3)
                {
                    Direction++;
                }
                else
                {
                    Direction = 0;
                }
            }
            switch (Direction)
            {
                case 0:
                    Y+= kms;
                    break;
                case 1:
                    X+= kms;
                    break;
                case 2:
                    Y-= kms;
                    break;
                case 3:
                    X-= kms;
                    break;
            }
        }

        public double GetActualFare()
        {
            return 6.5 + 3.56 * kmsTravelled;
        }

        public double GetBestFare()
        {
            return 6.5 + (Math.Abs(X) + Math.Abs(Y)) * 3.56;
        }
    }
}
