namespace SoundTraining.Provider.Dao.Implementations
{
    using Interfaces;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class DbSession : ISession
    {
        private MySqlConnection Connection { get; set; }

        private MySqlTransaction Transaction { get; set; }

        private List<IDbDataParameter> Parameters { get; set; }

        private readonly ISessionFactory sessionFactory;

        private readonly string connectionString;

        private bool invalidSession;

        public DbSession(string connStr, ISessionFactory sf)
        {
            Connection = new MySqlConnection(connStr);
            Parameters = new List<IDbDataParameter>();
            invalidSession = false;
            sessionFactory = sf;
            connectionString = connStr;
        }

        public void AddParameter<T>(string name, T value)
        {
            var par = new MySqlParameter(name, value);
            Parameters.Add(par);
        }

        public void AddParameter<T>(string name, T value, ParameterDirection direction)
        {
            var par = new MySqlParameter(name, value);
            par.Direction = direction;
            Parameters.Add(par);
        }

        public void Exec(string query)
        {
            using (var com = new MySqlCommand(query))
            {
                com.Connection = Connection;
                com.Transaction = Transaction;
                com.Parameters.Clear();
                foreach (var p in Parameters)
                {
                    com.Parameters.Add(p);
                }

                com.ExecuteNonQuery();
            }
        }

        public T Exec<T>(string query)
        {
            using (var com = new MySqlCommand(query))
            {
                com.Connection = Connection;
                com.Transaction = Transaction;

                foreach (var p in Parameters)
                {
                    com.Parameters.Add(p);
                }

                var result = com.ExecuteScalar();
                com.Parameters.Clear();

                if (result == null || result == DBNull.Value)
                {
                    return default(T);
                }

                return (T)Convert.ChangeType(result, typeof(T));
            }
        }

        public List<T> Exec<T>(string query, Func<DataSet, List<T>> mapper)
        {
            using (var com = new MySqlCommand(query))
            {
                com.Connection = Connection;
                com.Transaction = Transaction;

                foreach (var p in Parameters)
                {
                    com.Parameters.Add(p);
                }

                using (var ds = new DataSet())
                {
                    ds.EnforceConstraints = false;
                    using (var reader = com.ExecuteReader())
                    {
                        while (!reader.IsClosed)
                        {
                            ds.Load(reader, LoadOption.OverwriteChanges, string.Empty);
                        }
                    }

                    com.Parameters.Clear();
                    return mapper(ds);
                }
            }
        }

        public void Invalidate()
        {
            invalidSession = true;
        }

        public void Complete()
        {
            if (Transaction != null && Connection != null)
            {
                if (Connection.State == ConnectionState.Open)
                {
                    if (!invalidSession)
                    {
                        Transaction.Commit();
                    }
                    else
                    {
                        Transaction.Rollback();
                    }
                }

                Connection.Close();
                Transaction.Dispose();
                Connection.Dispose();
                Transaction = null;
                Connection = null;
                sessionFactory.ReleaseSession();
            }
        }

        public void Dispose()
        {
            Complete();
        }

        public void ClearParameters()
        {
            Parameters = new List<IDbDataParameter>();
        }

        public void Begin()
        {
            if (Connection == null)
            {
                Connection = new MySqlConnection(connectionString);
            }

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                Transaction = Connection.BeginTransaction();
            }
        }
    }
}
