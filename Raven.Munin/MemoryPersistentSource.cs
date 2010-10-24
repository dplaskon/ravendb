﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Raven.Munin
{
    public class MemoryPersistentSource : AbstractPersistentSource
    {
        private MemoryStream log;

        public MemoryPersistentSource()
        {
            log = new MemoryStream();
            CreatedNew = true;
        }

        protected override Stream CreateClonedStreamForReadOnlyPurposes()
        {
            return new MemoryStream(log.GetBuffer(), 0, (int) Log.Length, false);
        }

        public MemoryPersistentSource(byte[] data)
        {
            log = new MemoryStream(data);
            CreatedNew = true;
        }

        protected override Stream Log { get { return log; } }

        #region IPersistentSource Members

        public override void ReplaceAtomically(Stream log)
        {
            log = (MemoryStream)log;
        }

        public override Stream CreateTemporaryStream()
        {
            return new MemoryStream();
        }

        public override void FlushLog()
        {
        }

        public override RemoteManagedStorageState CreateRemoteAppDomainState()
        {
            return new RemoteManagedStorageState
            {
                Log = ((MemoryStream) Log).ToArray(),
            };
        }

        #endregion
    }
}