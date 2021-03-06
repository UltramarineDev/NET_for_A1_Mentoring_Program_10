﻿using System;

namespace ExceptionHandlingTask2
{
    public class StringConverter
    {
        public int ConvertToInteger(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "Input can not be null or empty.");
            }

            return GetIntegerFromString(input);
        }

        private int GetIntegerFromString(string input)
        {
            int output = 0;
            bool isNegative = false;

            foreach (var ch in input)
            {
                if (((int)ch < 48 || (int)ch > 57) && (int)ch != 45)
                {
                    throw new ArgumentException(nameof(input), "Input string should contain only digits or minus symbol.");
                }

                if ((int)ch == 45)
                {
                    isNegative = true;
                    continue;
                }

                output *= 10;
                output += ch - '0';
            }

            return isNegative ? output * (-1) : output;
        }
    }
}
