using System;
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
       
        public SuggestionQueryResult ExecuteSuggestionQuery(SuggestionQuery suggestionQuery)
        {
            if (suggestionQuery == null) throw new ArgumentNullException("suggestionQuery");
            if (string.IsNullOrWhiteSpace(suggestionQuery.Term)) throw new ArgumentNullException("suggestionQuery.Term");
            if (string.IsNullOrWhiteSpace(suggestionQuery.IndexName)) throw new ArgumentNullException("suggestionQuery.IndexName");
            if (string.IsNullOrWhiteSpace(suggestionQuery.Field)) throw new ArgumentNullException("suggestionQuery.Field");
            if (suggestionQuery.MaxSuggestions <= 0) suggestionQuery.MaxSuggestions = 10;
            if (suggestionQuery.Accuracy <= 0 || suggestionQuery.Accuracy > 1) suggestionQuery.Accuracy = 0.5f;

            var indexReader = _database.IndexStorage.GetIndexReader(suggestionQuery.IndexName);
            var directory = indexReader.Directory();

            var spellChecker = new SpellChecker.Net.Search.Spell.SpellChecker(directory, GetStringDistance(suggestionQuery));
            spellChecker.SetAccuracy(suggestionQuery.Accuracy);

            var suggestions = spellChecker.SuggestSimilar(suggestionQuery.Term, suggestionQuery.MaxSuggestions, indexReader,
                                        suggestionQuery.Field, true);

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