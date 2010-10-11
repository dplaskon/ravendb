using System.Collections.Generic;

namespace Raven.Database.Data
{
    /// <summary>
    /// The result of the suggestion query
    /// </summary>
    public class SuggestionQueryResult
    {
         /// <summary>
        /// The suggestions based on the term and dictionary
        /// </summary>
        /// <value>The suggestions.</value>
        public string[] Suggestions { get; set; } 
    }
}