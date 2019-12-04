namespace SoundTraining.WebApi.Controllers
{
    using Provider.Dao.Interfaces;
    using Provider.Services.Interfaces;
    using System;
    using System.Web.Http;

    public class UserController : BaseApiController
    {
        private readonly IDaoService daoService;

        public UserController(
            ISessionFactory sessionFactory,
            IDaoService daoSrv) : base(sessionFactory)
        {
            daoService = daoSrv;
        }

        [HttpGet]
        [Route("api/users", Name = "Users")]
        public IHttpActionResult Get()
        {
            try
            {
                return CatchException(() =>
                {
                    var response = daoService.GetAllUsers();
                    return Ok(response);
                });
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return InternalServerError();
            }
        }
    }
}