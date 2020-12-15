using System;
using System.Collections.Generic;
using System.Linq;

namespace wrapping_paper
{
    public class PaperCalculator
    {
        public int GetTotalSquareFeet(IEnumerable<string> gifts)
        {
          int sqFeet = 0;
          foreach(string line in gifts)
          {
            (int l, int w, int h) dims = GetDimensions(line);
            int[] sides = {dims.l * dims.w, dims.l * dims.h, dims.w * dims.h};
            int smallestSide = sides.Min();
            int giftPaper = sides.Sum() * 2 + smallestSide;
            sqFeet+= giftPaper;
          }
          return sqFeet;
        }

        private (int l, int w, int h) GetDimensions(string line)
        {          
          int[] numbers = line.Trim().Split("x").Select(i => int.Parse(i)).ToArray();
          return (numbers[0], numbers[1], numbers[2]);
        }
    }
}