namespace SoundTraining.Provider.Dao.Interfaces
{
    using Domain;
    using System.Collections.Generic;

    public interface IHistoryDao
    {
        List<History> GetAll();

        long Insert(History history);

        List<History> GetHistoryByUserID(int userID);

        List<History> GetHistoryByAppID(int appID);

        List<FrequencyPercentaje> GetFrequencyCountByUserID(int userID);

        List<FrequencyPercentaje> GetFrequencyCount();
    }
}
