using System;
using Xunit;
using wrapping_paper;
using System.IO;
using System.Collections.Generic;

namespace PaperCalaulatorTest
{
    public class UnitTest1
    {
        [Fact]
        public void PaperCalculatorTestExample()
        {
            var c = new PaperCalculator();
            IEnumerable<string> lines = File.ReadLines("C:\\dev\\advent\\wrapping-paper\\PaperCalaulatorTest\\example.txt");
            int sf = c.GetTotalSquareFeet(lines);

            Assert.Equal(sf, 101);
        }
        
    }
}
