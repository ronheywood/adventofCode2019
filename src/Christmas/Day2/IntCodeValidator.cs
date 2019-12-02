using System.Linq;

namespace Christmas.Day2
{
    public class IntCodeValidator
    {
        public virtual bool Validate(string input) => input.Contains(",") && input.Split(',').All(i => int.TryParse(i, out var ignored));
    }
}
