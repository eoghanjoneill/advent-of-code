using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace csi_csharp
{
    public class MapBuilder
    {
        public IEnumerable<string> GetMap(string path)
        {
          IEnumerable<string> lines = File.ReadLines(path);
          var map = new char[20,40]; //20 rows, 40 cols
          foreach (string line in lines)
          {
            string[] instructions = line.Trim().Split(",");
            int x = int.Parse(instructions[0]);
            int y = int.Parse(instructions[1]);
            string text = instructions[2];
            int offset = 0;
            foreach (char c in text) {
              map[y,x + offset] = c;
              offset++;
            }
          }
          List<string> outLines = new List<string>();
          string headerRow = "  ";
          for (int i = 0; i < 40; i++) 
          {
            headerRow += i%10;
          }
          outLines.Add(headerRow);
          for(int y = 0; y < 20; y++) {            
            char[] chars =  Enumerable.Range(0, map.GetLength(1))
                .Select(x => map[y, x])
                .ToArray();
            string outLine = y.ToString().PadLeft(2,' ') + new String(chars);            
            outLines.Add(outLine);
          }
          return outLines;
        } 
    }
}
