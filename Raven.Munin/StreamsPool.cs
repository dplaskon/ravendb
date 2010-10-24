using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Linq;

namespace Raven.Munin
{
    public class StreamsPool : IDisposable
    {
        private readonly Func<Stream> createNewStream;
        private readonly ConcurrentDictionary<int, ConcurrentQueue<Stream>> openedStreamsPool = new ConcurrentDictionary<int, ConcurrentQueue<Stream>>();
        private int version;

        public StreamsPool(Func<Stream> createNewStream)
        {
            this.createNewStream = createNewStream;
            openedStreamsPool.TryAdd(0, new ConcurrentQueue<Stream>());
        }

        public int Count
        {
            get
            {
                return openedStreamsPool.Sum(x => x.Value.Count);
            }
        }

        public void Clear()
        {
            Stream result;
            var currentVersion = Interlocked.Increment(ref version);
            openedStreamsPool.TryAdd(currentVersion, new ConcurrentQueue<Stream>());
            var keysToRemove = openedStreamsPool.Keys.Where(x => x < currentVersion).ToArray();

            foreach (var keyToRemove in keysToRemove)
            {
                ConcurrentQueue<Stream> value;
                if (openedStreamsPool.TryRemove(keyToRemove, out value) == false)
                    continue;

                while (value.TryDequeue(out result))
                {
                    result.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Stream result;
            var currentVersion = Interlocked.Increment(ref version);
            openedStreamsPool.TryAdd(currentVersion, new ConcurrentQueue<Stream>());
            var keysToRemove = openedStreamsPool.Keys.ToArray();

            foreach (var keyToRemove in keysToRemove)
            {
                ConcurrentQueue<Stream> value;
                if (openedStreamsPool.TryRemove(keyToRemove, out value) == false)
                    continue;

                while (value.TryDequeue(out result))
                {
                    result.Dispose();
                }
            }
        }

        public IDisposable Use(out Stream stream)
        {
            var currentversion = Thread.VolatileRead(ref version);
            ConcurrentQueue<Stream> current;
            openedStreamsPool.TryGetValue(currentversion, out current);
            Stream value = current != null && current.TryDequeue(out value) ? 
                value : 
                createNewStream();
            stream = value;
            return new DisposableAction(delegate
            {
                ConcurrentQueue<Stream> current2;
                if (currentversion == Thread.VolatileRead(ref currentversion) && 
                    openedStreamsPool.TryGetValue(currentversion, out current2))
                {
                    current2.Enqueue(value);
                }
                else
                {
                    value.Dispose();
                }
            });
        }

        /// <summary>
        /// A helper class that translate between Disposable and Action
        /// </summary>
        public class DisposableAction : IDisposable
        {
            private readonly Action action;

            /// <summary>
            /// Initializes a new instance of the <see cref="DisposableAction"/> class.
            /// </summary>
            /// <param name="action">The action.</param>
            public DisposableAction(Action action)
            {
                this.action = action;
            }

            /// <summary>
            /// Execute the relevant actions
            /// </summary>
            public void Dispose()
            {
                action();
            }
        }
    }
}