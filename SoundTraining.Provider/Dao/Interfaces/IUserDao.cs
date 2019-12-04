namespace SoundTraining.Provider.Dao.Interfaces
{
    using Domain;
    using System.Collections.Generic;

    public interface IUserDao
    {
        List<User> GetAll();
    }
}
