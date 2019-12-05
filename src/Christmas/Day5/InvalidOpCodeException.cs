using System;

namespace Christmas.Day5
{
    public class InvalidOpCodeException : Exception
    {
        public InvalidOpCodeException(string message) : base(message){}
    }
}