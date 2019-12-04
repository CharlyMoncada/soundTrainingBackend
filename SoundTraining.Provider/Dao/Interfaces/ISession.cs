namespace SoundTraining.Provider.Dao.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface ISession : IDisposable
    {
        void AddParameter<T>(string name, T value);

        void AddParameter<T>(string name, T value, ParameterDirection direction);

        T Exec<T>(string query);

        List<T> Exec<T>(string query, Func<DataSet, List<T>> mapper);

        void Exec(string query);

        void Invalidate();

        void Complete();

        void ClearParameters();

        void Begin();
    }
}
