namespace SoundTraining.Provider.Services.Interfaces
{
    using Domain;
    using System.Collections.Generic;

    public interface IDaoService
    {
        bool CheckDatabaseConnection();

        List<User> GetAllUsers();

        List<History> GetAllHistory();

        bool InsertHistory(History history);

        List<History> GetAllHistoryByUserID(int userID);

        FrequencyByUser GetFrequencyByUser(int userID);

        TotalFrecuency GetFrequencyCount();
    }
}
