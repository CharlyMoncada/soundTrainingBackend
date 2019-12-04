namespace SoundTraining.Provider.Dao.Implementations
{
    using Interfaces;
    using Domain;
    using System.Collections.Generic;
    using System.Data;
    using System;
    using System.Linq;

    public class UserDao : BaseDao, IUserDao
    {
        public UserDao(ISessionFactory sf) : base(sf)
        {

        }

        public List<User> GetAll()
        {
            var sql = "SELECT id AS userId, user AS userName, first_name AS firstName, last_name AS lastName, email, age, date_created AS dateCreated FROM tbl_user;";

            return CatchException(() =>
            {
                return Exec(
                    sql,
                    dataset =>
                    {
                        return BuildUsersList(dataset);
                    });
            });
        }

        private List<User> BuildUsersList(DataSet dataset)
        {
            return (from userDR in dataset.Tables[0].AsEnumerable()
                    select new User()
                    {
                        ID = DefaultOrValue<int>(userDR["userId"]),
                        Age = DefaultOrValue<int>(userDR["age"]),
                        DateCreated = DefaultOrValue<DateTime>(userDR["dateCreated"]),
                        Email = DefaultOrValue<string>(userDR["email"]),
                        FirstName = DefaultOrValue<string>(userDR["firstName"]),
                        LastName = DefaultOrValue<string>(userDR["lastName"]),
                        UserName = DefaultOrValue<string>(userDR["userName"])
                    }).ToList();
        }
    }
}
