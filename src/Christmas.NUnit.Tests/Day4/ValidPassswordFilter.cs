using System.Collections.Generic;
using System.IO;
using Christmas.Day4;

namespace Christmas.NUnit.Tests.Day4
{
    public class ValidPassswordFilter
    {
        public static int GetValidPasswords(int start,int end)
        {
            var count = 0;
            for (var i = start; i <= end; i++)
            {
                count += PasswordValidation.Validate(i.ToString()) ? 1 : 0;
            }

            return count;
        }
    }
}
