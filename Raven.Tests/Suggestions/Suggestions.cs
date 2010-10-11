using System;
using Raven.Client.Document;
using Raven.Client.Tests;
using Xunit;

namespace Raven.Tests.Suggestions
{
    public class Suggestions : RemoteClientTest, IDisposable
    {
        private string path;

        #region IDisposable Members

        public void Dispose()
        {
           
        }

        #endregion

        public Suggestions()
        {
            
        }

        protected DocumentStore DocumentStore { get; set; }

        [Fact]
        public void CanGetSuggestionsUsing_Levenshtein()
        {}
    }
}
