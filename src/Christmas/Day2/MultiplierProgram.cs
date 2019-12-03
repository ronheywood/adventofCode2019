﻿using System;
using System.Linq;

namespace Christmas.Day2
{
    public class MultiplierProgram : IIntCodeProgram
    {
        private readonly IIntCodeValidator _validator;

        public MultiplierProgram(IIntCodeValidator validator)
        {
            _validator = validator;
        }

        public string Process(string input, int startIndex = 0)
        {
            if (!_validator.Validate(input))  throw new Exception("");
            var intList = _validator.SplitString(input).ToArray();
            
            if (intList[startIndex]!=2) throw new Exception("Invalid input: op code (first digit) must be 2");
            _validator.ExtractOrdinals(intList, out var i1, out var i2, out var pointer,startIndex:startIndex);

            intList[pointer] = intList[i1] * intList[i2];
            return _validator.Join(intList);
        }
    }
}