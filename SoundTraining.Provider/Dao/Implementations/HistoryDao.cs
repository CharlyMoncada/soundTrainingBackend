namespace SoundTraining.Provider.Dao.Implementations
{
    using Interfaces;
    using Domain;
    using System.Collections.Generic;
    using System.Data;
    using System;
    using System.Linq;

    public class HistoryDao : BaseDao, IHistoryDao
    {
        public HistoryDao(ISessionFactory sf) : base(sf)
        {

        }

        public List<History> GetAll()
        {
            var sql = "SELECT id AS historyId, frequency, user_id AS userId, app_id as appId, date_saved AS dateSaved FROM tbl_history;";

            return CatchException(() =>
            {
                return Exec(
                    sql,
                    dataset =>
                    {
                        return BuildHistoryList(dataset);
                    });
            });
        }

        public List<History> GetHistoryByUserID(int userID)
        {
            var sql = $"SELECT id AS historyId, frequency, user_id AS userId, app_id as appId, date_saved AS dateSaved FROM tbl_history WHERE user_id = {userID};";

            return CatchException(() =>
            {
                return Exec(
                    sql,
                    dataset =>
                    {
                        return BuildHistoryList(dataset);
                    });
            });
        }

        public List<History> GetHistoryByAppID(int appID)
        {
            var sql = $"SELECT id AS historyId, frequency, user_id AS userId, app_id as appId, date_saved AS dateSaved FROM tbl_history WHERE app_id = {appID};";

            return CatchException(() =>
            {
                return Exec(
                    sql,
                    dataset =>
                    {
                        return BuildHistoryList(dataset);
                    });
            });
        }

        public long Insert(History history)
        {
            var sql = @"INSERT INTO tbl_history (frequency, user_id, app_id, date_saved) 
                        VALUES (@frequency, @user_id, @app_id, @date_saved);
                        SELECT LAST_INSERT_ID();";

            return CatchException(() =>
            {
                AddParameter("@frequency", history.Frequency);
                AddParameter("@user_id", history.ID);
                AddParameter("@app_id", history.AppType);
                AddParameter("@date_saved", DateTime.UtcNow);

                return Exec<long>(sql);
            });
        }

        public List<FrequencyPercentaje> GetFrequencyCountByUserID(int userID)
        {
            var sql = $"Select h.frequency, count(*) as total from tbl_history h where h.user_id = {userID} group by h.frequency order by h.frequency asc"; 

            return CatchException(() =>
            {
                return Exec(
                    sql,
                    dataset =>
                    {
                        return (from userDR in dataset.Tables[0].AsEnumerable()
                                select new FrequencyPercentaje()
                                {
                                    Frecuency = DefaultOrValue<int>(userDR["frequency"]),
                                    Total = DefaultOrValue<int>(userDR["total"])
                                }).ToList();
                    });
            });
        }

        public List<FrequencyPercentaje> GetFrequencyCount()
        {
            var sql = "Select h.frequency, count(*) as total from tbl_history h group by h.frequency order by h.frequency asc";

            return CatchException(() =>
            {
                return Exec(
                    sql,
                    dataset =>
                    {
                        return (from userDR in dataset.Tables[0].AsEnumerable()
                                select new FrequencyPercentaje()
                                {
                                    Frecuency = DefaultOrValue<int>(userDR["frequency"]),
                                    Total = DefaultOrValue<int>(userDR["total"])
                                }).ToList();
                    });
            });
        }

        private List<History> BuildHistoryList(DataSet dataset)
        {
            return (from userDR in dataset.Tables[0].AsEnumerable()
                    select new History()
                    {
                        ID = DefaultOrValue<int>(userDR["userId"]),
                        AppType = (AppTypes)DefaultOrValue<int>(userDR["appId"]),
                        DateSaved = DefaultOrValue<DateTime>(userDR["dateSaved"]),
                        Frequency = DefaultOrValue<int>(userDR["frequency"]),
                        UserID = DefaultOrValue<int>(userDR["userId"])
                    }).ToList();
        }
    }
}
