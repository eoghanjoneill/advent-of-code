using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace waypoints
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MapTraverse m = new MapTraverse("C:\\dev\\advent\\waypoints\\example.txt");
            string s = m.Navigate();
            Console.WriteLine($"Waypoints: {s}");

            m = new MapTraverse("C:\\dev\\advent\\waypoints\\map.txt");
            s = m.Navigate();
            Console.WriteLine($"Waypoints: {s}");
        }

    
    }

    class MapTraverse
    {
        public Dictionary<Point, char> Map { get; }
        public Point StartPoint { get; }

        public MapTraverse(string filePath)
        {   
            this.Map = new Dictionary<Point, char>();     
            int y = 0;            
            foreach (string s in File.ReadLines(filePath))
            {
                int x = 0;
                foreach (char c in s.ToCharArray())
                {
                    if (y == 0 && c == Tiles.NS)
                    {
                        this.StartPoint = new Point{ X = x, Y = y};
                        Console.WriteLine($"Starting at loc: ({StartPoint.X},{StartPoint.Y})");
                    }   
                    if (c != ' ')
                    {
                        this.Map.Add(new Point { X = x, Y = y }, c);
                    }
                    x++;                
                }
                y++;
            }
        }

        private Point currentPoint;
        private char currentChar;
        private int currentDirection;  //N = 0, E = 1, S = 2, W = 3
        private StringBuilder sb;
        
        public string Navigate() {
            sb = new StringBuilder();
            //start at beginning - a very good place to start
            currentPoint = StartPoint;
            currentChar = Map[currentPoint];
            currentDirection = 2; //N = 0, E = 1, S = 2, W = 3
            while (MakeMove()) {

            }

            return sb.ToString();
        }

        private bool MakeMove()
        {
            //int N = 0, E = 1, S = 2, W = 3;
            
            //try getting next tile in current direction
            char nextChar;
            Point nextPoint;
            int nextDirection = currentDirection;
            if (currentChar == Tiles.JN)
            {
                int newDirectionLeft = TurnLeft(currentDirection);
                int newDirectionRight = TurnRight(currentDirection);
                //try turning left or right
                if (GetNextPoint(newDirectionLeft, out nextChar, out nextPoint))
                {
                    currentDirection = newDirectionLeft;
                    Console.WriteLine($"Turned left at JN loc: ({currentPoint.X},{currentPoint.Y})");
                }
                else if (GetNextPoint(newDirectionRight, out nextChar, out nextPoint))
                {
                    currentDirection = newDirectionRight;
                    Console.WriteLine($"Turned right at JN loc: ({currentPoint.X},{currentPoint.Y})");
                }
                else
                {
                    throw new Exception($"Nowhere to go at JN loc: ({currentPoint.X},{currentPoint.Y})");
                }                
            }
            else if (GetNextPoint(currentDirection, out nextChar, out nextPoint))
            {
                //keep going
            }
            else
            {
                Console.WriteLine($"End of road found at dir: {currentDirection}, loc: ({currentPoint.X},{currentPoint.Y}).");
                return false;
            }

            if (IsWaypoint(nextChar)) {
                sb.Append(nextChar);
            }
            currentChar = nextChar;
            currentPoint = nextPoint;
            return true;
        }

        private int TurnLeft (int dir)
        {            
            if (dir == 0) {
                return 3;
            } else {
                return dir - 1;
            }
        }
        
        private int TurnRight (int dir)
        {            
            if (dir == 3) {
                return 0;
            } else {
                return dir + 1;
            }
        }

        private bool IsWaypoint(char c) {
            if (c >= 'A' && c <= 'Z')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool GetNextPoint(int dir, out char c, out Point next)
        {            
            int step = 1;
            if (dir == (int)Compass.N || dir == (int)Compass.W) step = -1;
            if (dir == (int)Compass.S || dir == (int)Compass.N)
            {                
                next = new Point { Y = currentPoint.Y + step, X = currentPoint.X};
            }
            else
            {
                next = new Point { Y = currentPoint.Y, X = currentPoint.X + step };
            }
           if (Map.ContainsKey(next)){
               c = Map[next];
               return true;
           }
           else
           {
               c = ' ';
               return false;
           }
        }        
    }

    public static class Tiles
    {
        public const char NS = '|';
        public const char EW = '-';
        public const char JN = '+';
    }

    enum Compass
    {
        N = 0,
        E = 1,
        S = 2,
        W = 3
    }

    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }        
    }
}
