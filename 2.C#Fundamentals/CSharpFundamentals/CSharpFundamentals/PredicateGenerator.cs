using System.Linq;

namespace CSharpFundamentals
{
    public class PredicateGenerator : IPredicateGenerator
    {
        private readonly string _pattern;
        public PredicateGenerator(string pattern)
        {
            _pattern = pattern;
        }

        public bool GetPredicate(string entry)
            => entry.Split('\\').Last().Contains(_pattern);
    }
}
