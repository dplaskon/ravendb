using System.Collections.Generic;

namespace Raven.Database.Data
{
    /// <summary>
    /// The result of the suggestion query
    /// </summary>
    public class SuggestionQueryResult : SuggestionQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestionQueryResult"/> class.
        /// </summary>
        public SuggestionQueryResult()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestionQueryResult"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        public SuggestionQueryResult(SuggestionQuery query)
            :base(query)
        {}
        
        /// <summary>
        /// The suggestions based on the term and dictionary
        /// </summary>
        /// <value>The suggestions.</value>
        public IEnumerable<string> Suggestions { get; set; } 
    }
}