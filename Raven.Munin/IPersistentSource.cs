﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Raven.Munin
{
    public interface IPersistentSource : IDisposable
    {
        T Read<T>(Func<Stream,T> readOnlyAction);

        T Read<T>(Func<T> readOnlyAction);

        IEnumerable<T> Read<T>(Func<IEnumerable<T>> readOnlyAction);

        void Write(Action<Stream> readWriteAction);

        bool CreatedNew { get; }
        IList<PersistentDictionaryState> DictionariesStates { get; }

        void ReplaceAtomically(Stream log);

        Stream CreateTemporaryStream();

        void FlushLog();
        RemoteManagedStorageState CreateRemoteAppDomainState();
        void ClearPool();
    }
}