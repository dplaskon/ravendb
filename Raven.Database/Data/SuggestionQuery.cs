namespace Raven.Database.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class SuggestionQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestionQuery"/> class.
        /// </summary>
        public SuggestionQuery()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestionQuery"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        public SuggestionQuery(SuggestionQuery query)
        {
            this.Term = query.Term;
            this.IndexName = query.IndexName;
            this.Field = query.Field;
            this.NumberOfSuggestions = query.NumberOfSuggestions;
            this.Distance = query.Distance;
        }

        /// <summary>
        /// Gets or sets the term. The term is what the user likely entered, and will used as the basis of the suggestions.
        /// </summary>
        /// <value>The term.</value>
        public string Term { get; set; }
        /// <summary>
        /// Gets or sets the name of the index to use as the suggestion dictionary.
        /// </summary>
        /// <value>The name of the index.</value>
        public string IndexName { get; set; }
        /// <summary>
        /// Gets or sets the field to be used in conjunction with the index.
        /// </summary>
        /// <value>The field.</value>
        public string Field { get; set; }
        /// <summary>
        /// Gets or sets the number of suggestions to return.
        /// </summary>
        /// <value>The number of suggestions.</value>
        public int NumberOfSuggestions { get; set; }
        /// <summary>
        /// Gets or sets the string distance algorithm.
        /// </summary>
        /// <value>The distance.</value>
        public StringDistanceTypes Distance { get; set; }
    }
}