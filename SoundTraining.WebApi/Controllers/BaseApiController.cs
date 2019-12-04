namespace SoundTraining.WebApi.Controllers
{
    using log4net;
    using Provider.Dao.Interfaces;
    using System;
    using System.Web;
    using System.Web.Http;

    public class BaseApiController : ApiController
    {
        private readonly ISessionFactory sessionFactory;

        protected BaseApiController(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        protected T CatchException<T>(Func<T> func)
        {
            try
            {
                using (var session = sessionFactory.CreateSession())
                {
                    return func();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected string GetCurrentAuthToken()
        {
            try
            {
                return HttpContext.Current.Request.Headers["Authorization"];
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}