using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sleepy_guards
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ProcessLog("C:\\dev\\advent\\sleepy-guards\\example.txt");
            Console.WriteLine("*******************8");
            ProcessLog("C:\\dev\\advent\\sleepy-guards\\real-quiz.txt");
        }

        static void ProcessLog(string path)
        {
            IEnumerable<string> lines = File.ReadLines(path);

            Dictionary<int, Guard> guards = new Dictionary<int, Guard>();
            
            SortedDictionary<DateTimeOffset, string> instructions = new SortedDictionary<DateTimeOffset, string>();
            foreach (string line in lines)
            {
                DateTimeOffset d = DateTimeOffset.Parse(line.Substring(1,16));
                string s = line.Substring(19);
                instructions.Add(d,s);                
            }

            Guard currentGuard = null;
            Nap currentNap = null;

            foreach (var pair in instructions) {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
            foreach (var pair in instructions)
            {
                if (pair.Value.StartsWith("Guard #"))
                {
                    int h = pair.Value.IndexOf('#');
                    int guardId = int.Parse(pair.Value.Substring(h+1, pair.Value.IndexOf(' ', h) - h - 1));
                    Console.WriteLine($"Guard #{guardId}");
                    if (currentGuard?.Id == guardId)
                    {
                        Console.WriteLine($"We already knew guard {guardId} was on duty!");
                    }
                    if (guards.ContainsKey(guardId))
                    {
                        currentGuard = guards[guardId];
                    }
                    else
                    {
                        currentGuard = new Guard(guardId);
                        guards.Add(guardId, currentGuard);
                    }
                }
                else if (pair.Value.StartsWith("falls"))
                {
                    if (currentNap != null)
                    {
                        throw new Exception($"{currentGuard?.Id} is already asleep!");
                    }
                    currentNap = new Nap();
                    currentNap.Date = pair.Key.Date;
                    currentNap.HourStart = pair.Key.Hour;
                    if (pair.Key.Hour != 0) {
                        throw new Exception("Not asleep in midnight hour?");
                    }
                    currentNap.MinStart = pair.Key.Minute;
                }
                else if (pair.Value.StartsWith("wakes"))
                {
                    if (currentNap == null) { throw new Exception("can't wake when not asleep"); }
                    if (currentNap.Date != pair.Key.Date) { throw new Exception("unexpected wake date"); }
                    currentNap.HourEnd = pair.Key.Hour;
                    if (pair.Key.Hour != 0) {
                        throw new Exception("Not waking in midnight hour?");
                    }
                    currentNap.MinEnd = pair.Key.Minute;
                    if (currentNap.MinStart > currentNap.MinEnd) { throw new Exception("wake before fall asleep?"); }
                    currentNap.Duration = currentNap.MinEnd - currentNap.MinStart;
                    
                    currentGuard.Naps.Add(currentNap);
                    currentNap = null;
                }
                else { throw new Exception("what???"); }
            }

            
            IEnumerable<Guard> orderedGuards = guards.Select(g => g.Value).OrderBy(g => g.GetTotalSleepTime());
            foreach (var guard in orderedGuards) {
                Console.WriteLine($"Guard {guard.Id} slept for {guard.GetTotalSleepTime()}");
            }

            Guard g = orderedGuards.Last();
            Console.WriteLine($"More info on {g.Id}:");
            Dictionary<int, int> sleepMins = new Dictionary<int, int>();
            for (int j = 0; j <60; j++)
            {
                sleepMins.Add(j,0);
            }
            foreach (Nap n in g.Naps)
            {
                for (int i = n.MinStart; i < n.MinEnd; i++)
                {
                    sleepMins[i] = sleepMins[i] + 1;
                }
            }
            
            int max = 0;
            int maxMinute = -1;
            foreach (var min in sleepMins)
            {
                Console.WriteLine ($"in minute {min.Key} he was asleep {min.Value} times" );
                if (min.Value > max)
                {
                    max = min.Value;
                    maxMinute = min.Key;
                }
            }

            Console.WriteLine ($"***** TOP MIN: in minute {maxMinute} he was asleep {max} times" );
        }
    }

    class Guard
    {
        public int Id { get; set; }
        public List<Nap> Naps { get; set; }
        
        public Guard(int guardId)
        {
            Id = guardId;
            Naps = new List<Nap>();
        }
         
        public int GetTotalSleepTime() {
            return Naps.Aggregate(0, (acc, x) => acc += x.Duration);
        }
    }

    class Nap
    {
        //Guards count as asleep on the minute they fall asleep, and they count as awake on the minute they wake up.
        public DateTime Date { get; set; }
        public int HourStart { get; set; }
        public int MinStart { get; set; }
        public int HourEnd { get; set; }
        public int MinEnd { get; set; }
        public int Duration { get; set; }

    }
}
