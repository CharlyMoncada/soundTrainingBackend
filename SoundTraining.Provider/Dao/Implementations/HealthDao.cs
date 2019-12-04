namespace SoundTraining.Provider.Dao.Implementations
{
    using Interfaces;
    using System;

    public class HealthDao : BaseDao, IHealthDao
    {
        private const string VerifycationQuery = "select database();";

        public HealthDao(ISessionFactory sf) : base(sf)
        {
        }

        public bool CheckStatus()
        {
            var databaseConnectionOk = false;

            try
            {
                CatchException(() =>
                {
                    var databaseName = Exec<string>(VerifycationQuery);
                    databaseConnectionOk = databaseName.Length > 0;
                });
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
            }

            return databaseConnectionOk;
        }

    }
}
