using System.Linq;

namespace Christmas.Day4
{
    public class PasswordSequencePairedValidation : PasswordSequenceValidator
    {
        public override bool ValidateAdjacent(string password)
        {   
            if(!base.ValidateAdjacent(password)) return false;
            var charArray = password.ToCharArray();
            return charArray.Any(c1 => password.ToCharArray().Count(c2 => c2==c1)==2);
        }
    }
}