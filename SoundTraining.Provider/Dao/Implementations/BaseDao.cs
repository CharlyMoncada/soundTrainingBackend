namespace SoundTraining.Provider.Dao.Implementations
{
    using Interfaces;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public abstract class BaseDao
    {
        private readonly ISessionFactory sessionFactory;

        private ISession session;

        //protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BaseDao(
            ISessionFactory sf)
        {
            sessionFactory = sf;
        }

        protected void AddParameter<T>(string name, T value)
        {
            session.AddParameter(name, value);
        }

        protected void AddParameter<T>(string name, T value, ParameterDirection direction)
        {
            session.AddParameter(name, value, direction);
        }

        protected T Exec<T>(string query)
        {
            var result = session.Exec<T>(query);
            ClearParameters();
            return result;
        }

        protected List<T> Exec<T>(string query, Func<DataSet, List<T>> mapper)
        {
            var result = session.Exec(query, mapper);
            ClearParameters();
            return result;
        }

        protected void Exec(string query)
        {
            session.Exec(query);
            ClearParameters();
        }

        protected void CatchException(Action func)
        {
            CatchException(() =>
            {
                func();
                return true;
            });
        }

        protected void ClearParameters()
        {
            session.ClearParameters();
        }

        protected T CatchException<T>(Func<T> func)
        {
            try
            {
                EnsureSession();
                session.Begin();
                session.ClearParameters();
                return func();
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                session.Invalidate();
                throw;
            }
        }

        protected T DefaultOrValue<T>(object value)
        {
            return DefaultOrValue(value, default(T));
        }

        protected T DefaultOrValue<T>(object value, T defaultValue)
        {
            if (value == null || value == DBNull.Value)
            {
                return defaultValue;
            }

            var type = typeof(T);
            type = Nullable.GetUnderlyingType(type) ?? type;

            return (T)Convert.ChangeType(value, type);
        }

        private void EnsureSession()
        {
            session = sessionFactory.CreateSession();
        }
    }
}
