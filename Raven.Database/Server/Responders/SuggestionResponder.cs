using System;
using Raven.Database.Data;
using Raven.Database.Server.Abstractions;

namespace Raven.Database.Server.Responders
{
    public class SuggestionResponder : RequestResponder
    {
        public override string UrlPattern
        {
            ///suggestion?term={0}&index={1}&field={2}&numOfSuggestions={3}&distance={4}
            get { return "/suggestion?(.+)"; }
        }

        public override string[] SupportedVerbs
        {
            get { return new[] {"GET"}; }
        }

        public override void Respond(IHttpContext context)
        {
            var term = context.Request.QueryString["term"];
            var index = context.Request.QueryString["index"];
            var field = context.Request.QueryString["field"];

            StringDistanceTypes distanceTypes;
            int numOfSuggestions;

            try {
                var distance = context.Request.QueryString["distance"];
                distanceTypes = (StringDistanceTypes) Enum.Parse(typeof (StringDistanceTypes), distance, true);
            }
            catch (Exception) {
                distanceTypes = StringDistanceTypes.Default;
            }

            try {
                var num = context.Request.QueryString["numOfSuggestions"];
                numOfSuggestions = int.Parse(num);
            }
            catch (Exception) {
                numOfSuggestions = 10;
            }

            var query = new SuggestionQuery()
                            {
                                Distance = distanceTypes,
                                Field = field,
                                IndexName = index,
                                NumberOfSuggestions = numOfSuggestions,
                                Term = term
                            };

            var suggestionQueryResult = Database.ExecuteSuggestionQuery(query);
            context.WriteJson(suggestionQueryResult);
        }
    }
}