using Raven.Database.Data;
using SpellChecker.Net.Search.Spell;

namespace Raven.Database
{
    public class SuggestionQueryRunner
    {
        private readonly DocumentDatabase _database;

        public SuggestionQueryRunner(DocumentDatabase database)
        {
            _database = database;
        }
       
        public SuggestionQueryResult ExecuteSuggestQuery(SuggestionQuery suggestionQuery)
        {
            var indexReader = _database.IndexStorage.GetIndexReader(suggestionQuery.IndexName);
            var directory = indexReader.Directory();

            var spellChecker = new SpellChecker.Net.Search.Spell.SpellChecker(directory, GetStringDistance(suggestionQuery));
            spellChecker.IndexDictionary(new LuceneDictionary(indexReader, suggestionQuery.Field));

            var suggestions = spellChecker.SuggestSimilar(suggestionQuery.Term, suggestionQuery.NumberOfSuggestions);

            return new SuggestionQueryResult(suggestionQuery) { Suggestions = suggestions};
        }

        private static StringDistance GetStringDistance(SuggestionQuery query)
        {
            switch (query.Distance)
            {
                case StringDistanceTypes.NGram:
                    return new NGramDistance();
                case StringDistanceTypes.JaroWinkler:
                    return new JaroWinklerDistance();
                default:
                    return new LevenshteinDistance();
            }
        }
    }
}