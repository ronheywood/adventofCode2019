using System.Linq;
using NUnit.Framework.Constraints;

namespace Christmas.Day4
{
    public static class PasswordValidation
    {
        public static bool ValidateFormat(string password)
        {
            if (password.Length != 6) return false;
            if(!int.TryParse(password, out var i1)) return false;
            if (i1.ToString() != password) return false;
            
            var charArray = password.ToCharArray();
            return !charArray.Where((t, i) => charArray.Length!=i+1 && t > charArray[i+1]).Any();
        }

        public static bool ValidateAdjacent(string password)
        {
            var charArray = password.ToCharArray();
            return charArray.Where((t, i) => charArray.Length!=i+1 && t == charArray[i+1]).Any();
        }

        public static bool Validate(string password) => ValidateFormat(password) && ValidateAdjacent(password);
    }
}
