using System;
using Raven.Database.Data;
using Raven.Database.Server.Abstractions;

namespace Raven.Database.Server.Responders
{
    public class SuggestionResponder : RequestResponder
    {
        public override string UrlPattern
        {
            ///suggest?term={0}&index={1}&field={2}&numOfSuggestions={3}&distance={4}&accuracy={5}
            get { return "/suggest?(.+)"; }
        }

        public override string[] SupportedVerbs
        {
            get { return new[] {"GET"}; }
        }

        /// <summary>
        /// Responds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Respond(IHttpContext context)
        {
            var term = context.Request.QueryString["term"];
            var index = context.Request.QueryString["index"];
            var field = context.Request.QueryString["field"];

            StringDistanceTypes distanceTypes;
            int numOfSuggestions;
            float accuracy;

            try {
                var distance = context.Request.QueryString["distance"];
                distanceTypes = (StringDistanceTypes) Enum.Parse(typeof (StringDistanceTypes), distance, true);
            }
            catch (Exception) {
                distanceTypes = StringDistanceTypes.Default;
            }

            try {
                var num = context.Request.QueryString["max"];
                numOfSuggestions = int.Parse(num);
            }
            catch (Exception) {
                numOfSuggestions = 0;
            }

            try {
                var accur = context.Request.QueryString["accuracy"];
                accuracy = float.Parse(accur);
            }
            catch (Exception) {
                accuracy = 0;
            }

            var query = new SuggestionQuery
                            {
                                Distance = distanceTypes,
                                Field = field,
                                IndexName = index,
                                MaxSuggestions = numOfSuggestions,
                                Term = term,
                                Accuracy = accuracy
                            };

            var suggestionQueryResult = Database.ExecuteSuggestionQuery(query);
            context.WriteJson(suggestionQueryResult);
        }
    }
}