using System;

namespace code_breaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Valid codes: " + getValidCodeCount().ToString());
        }
        static int getValidCodeCount() {
            int count = 0;
            const int min = 249905;
            const int max = 767253;
            for (int i = min; i <= max; i++) {
                if (testNumber(i)) {
                    count++;
                }
            }
            return count;
        }

        static bool testNumber(int num) {
            var chars = num.ToString().ToCharArray();
            if (!hasAdjacentDigits(chars)) {
                return false;
            }
            if (!digitsNeverDecrease(chars)) {
                return false;
            }
            return true;
        }

        static bool hasAdjacentDigits(char[] chars) {            
            for (int i = 1; i < chars.Length; i++) {
                if (chars[i] == chars[i - 1]) {
                    return true;
                }
            }
            return false;
        }

        static bool digitsNeverDecrease(char[] chars) {
            for (int i = 1; i < chars.Length; i++) {
                if (chars[i] < chars[i - 1]) {
                    return false;
                }
            }
            return true;
        }
    }
}
