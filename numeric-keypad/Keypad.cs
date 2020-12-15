using System;
using System.Collections.Generic;

namespace numeric_keypad
{
    public class Keypad
    {
        public string GetCode(IEnumerable<string> instructions) {
          string code = "";
          (int x, int y) pos = START_POS;
          foreach (string line in instructions) {
            pos = GetCoordinatesForLine(pos, line);
            code = code + keypadNumbers[pos.x,pos.y].ToString();
          }  
          return code;
        }

        private (int x, int y) GetCoordinatesForLine((int x, int y) pos, string line) {
          char[] chars = line.ToCharArray();
          
          foreach (char c in chars)
          {
            switch (c)
            {
              case 'U':
                if (pos.y > 0) {
                  pos.y--;
                }
                break;
              case 'D':
                if (pos.y < 2) {
                  pos.y++;
                }
                break;
              case 'L':
                if (pos.x > 0) {
                  pos.x--;
                }  
                break;
              case 'R':
                if (pos.x < 2) {
                  pos.x++;
                }
                break;
            }
          }
          return pos;
        }

        private int[,] keypadNumbers = new int[3,3] {{1,4,7},{2,5,8},{3,6,9}};
        private (int,int) START_POS = (1,1);
    }
}
