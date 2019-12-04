namespace Christmas.Day4
{
    public class ValidPasswordFilter
    {
        private readonly PasswordStructureValidator _structureValidation;
        private readonly PasswordSequenceValidator _sequenceValidation;

        public ValidPasswordFilter(PasswordStructureValidator structureValidation,PasswordSequenceValidator sequenceValidation)
        {
            _structureValidation = structureValidation;
            _sequenceValidation = sequenceValidation;
        }
        public int GetValidPasswords(int start,int end)
        {
            
            var passwordValidation = new PasswordValidation(_structureValidation,_sequenceValidation);
            var count = 0;
            for (var i = start; i <= end; i++)
            {
                count += passwordValidation.Validate(i.ToString()) ? 1 : 0;
            }

            return count;
        }
    }
}
