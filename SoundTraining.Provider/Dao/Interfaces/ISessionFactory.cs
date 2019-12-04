namespace SoundTraining.Provider.Dao.Interfaces
{
    public interface ISessionFactory
    {
        ISession CreateSession();

        void ReleaseSession();
    }
}
