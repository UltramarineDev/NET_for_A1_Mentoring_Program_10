using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryNuget
{
    public class NumberOperations
    {
        private NLogger logger;
        
        public NumberOperations()
        {
            logger = new NLogger();
        }
        
        public double Add(double x, double y)
        {
            var result = x + y;
            logger.Log(result.ToString());
            return result;
        }

        public double Substarct(double x, double y)
        {
            var result = x - y;
            logger.Log(result.ToString());
            return result;
        }

        public double DigitsCount(double number)
        {
            return Math.Floor(Math.Log10(number) + 1);
        }

        public bool IsDigitInNumber(int number, int digit)
        {
            return number.ToString().Contains(digit.ToString());
        }

        public IEnumerable<int> GetDigitsCollection(int number)
        {
            return Array.ConvertAll(number.ToString().ToArray(), x => (int)x);
        }
    }
}
