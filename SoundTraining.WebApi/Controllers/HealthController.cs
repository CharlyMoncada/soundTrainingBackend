namespace SoundTraining.WebApi.Controllers
{
    using Provider.Dao.Interfaces;
    using Provider.Services.Interfaces;
    using System.Net;
    using System.Web.Http;

    public class HealthController : BaseApiController
    {
        private readonly IDaoService daoService;

        public HealthController(
            ISessionFactory sessionFactory,
            IDaoService daoSrv)
            : base(sessionFactory)
        {
            daoService = daoSrv;
        }

        [HttpGet]
        [Route("api/Health", Name = "CheckConnection")]
        public IHttpActionResult Get()
        {
            var resultInfo = new
            {
                Message = CatchException(() => daoService.CheckDatabaseConnection())
            };

            return Content(HttpStatusCode.OK, resultInfo);
        }
    }
}