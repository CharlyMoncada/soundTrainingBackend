namespace SoundTraining.Provider.Services.Implementations
{
    using Dao.Interfaces;
    using Domain;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DaoService : IDaoService
    {
        private readonly IHealthDao healthDao;
        private readonly IUserDao userDao;
        private readonly IHistoryDao historyDao;

        public DaoService(
            IHealthDao healthD,
            IUserDao userD,
            IHistoryDao historyD)
        {
            healthDao = healthD;
            userDao = userD;
            historyDao = historyD;
        }

        public bool CheckDatabaseConnection()
        {
            return healthDao.CheckStatus();
        }

        public List<User> GetAllUsers()
        {
            return userDao.GetAll();
        }

        public List<History> GetAllHistory()
        {
            return historyDao.GetAll();
        }

        public List<History> GetAllHistoryByUserID(int userID)
        {
            return historyDao.GetHistoryByUserID(userID);
        }

        public bool InsertHistory(History history)
        {
            var id = historyDao.Insert(history);

            return id > 0 ? true : false;
        }

        public FrequencyByUser GetFrequencyByUser(int userID)
        {
            var frequencies = historyDao.GetFrequencyCountByUserID(userID);
            var totalReproductions = frequencies.Sum(x => x.Total);

            foreach (var frecuency in frequencies)
            {
                frecuency.Percentaje = Math.Round(Convert.ToDouble((frecuency.Total * 100) / totalReproductions), 2);
            }

            var result = new FrequencyByUser()
            {
                UserID = userID,
                Frequency = frequencies
            };

            return result;
        }

        public TotalFrecuency GetFrequencyCount()
        {
            var frequencies = historyDao.GetFrequencyCount();
            var totalReproductions = frequencies.Sum(x => x.Total);

            foreach (var frecuency in frequencies)
            {
                frecuency.Percentaje = Math.Round(Convert.ToDouble((frecuency.Total * 100) / totalReproductions), 2);
            }

            var result = new TotalFrecuency()
            {
                Frecuency = frequencies
            };

            return result;
        }
    }
}
