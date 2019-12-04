using System.Linq;

namespace Christmas.Day4
{
    public class PasswordValidation
    {
        private readonly PasswordStructureValidator _structureValidate;
        private readonly PasswordSequenceValidator _sequenceValidator;

        public PasswordValidation(PasswordStructureValidator structureValidate, PasswordSequenceValidator sequenceValidator)
        {
            _structureValidate = structureValidate;
            _sequenceValidator = sequenceValidator;
        }
        public bool ValidateFormat(string password)
        {
            if (password.Length != 6) return false;
            if(!int.TryParse(password, out var i1)) return false;
            if (i1.ToString() != password) return false;
            
            var charArray = password.ToCharArray();
            return !charArray.Where((t, i) => charArray.Length!=i+1 && t > charArray[i+1]).Any();
        }

        public bool ValidateAdjacent(string password)
        {
            var charArray = password.ToCharArray();
            return charArray.Where((t, i) => charArray.Length!=i+1 && t == charArray[i+1]).Any();
        }

        public bool Validate(string password) => _structureValidate.ValidateFormat(password) && _sequenceValidator.ValidateAdjacent(password);
    }

    public class PasswordStructureValidator
    {  
        public bool ValidateFormat(string password)
        {
            if (password.Length != 6) return false;
            if(!int.TryParse(password, out var i1)) return false;
            if (i1.ToString() != password) return false;
            
            var charArray = password.ToCharArray();
            return !charArray.Where((t, i) => charArray.Length!=i+1 && t > charArray[i+1]).Any();
        }
    }

    public class PasswordSequenceValidator
    {
        public virtual bool ValidateAdjacent(string password)
        {
            var charArray = password.ToCharArray();
            return charArray.Where((t, i) => charArray.Length!=i+1 && t == charArray[i+1]).Any();
        }
    }
}
