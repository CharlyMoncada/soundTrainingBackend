namespace SoundTraining.Provider.Dao.Implementations
{
    using Interfaces;
    using System.Collections.Concurrent;
    using System.Threading;

    public class DbSessionFactory : ISessionFactory
    {
        public string ConnectionString { get; set; }

        private ConcurrentDictionary<int, ISession> Sessions { get; set; }

        private static object syncObject = new object();

        public DbSessionFactory()
        {
            Sessions = new ConcurrentDictionary<int, ISession>();
        }

        public ISession CreateSession()
        {
            lock (syncObject)
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                ISession ses = null;

                if (!Sessions.TryGetValue(threadId, out ses))
                {
                    ses = new DbSession(ConnectionString, this);
                    Sessions.TryAdd(threadId, ses);
                }

                return ses;
            }
        }

        public void ReleaseSession()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            ISession ses = null;

            if (Sessions.TryRemove(threadId, out ses))
            {
                ses.Complete();
            }
        }
    }
}
