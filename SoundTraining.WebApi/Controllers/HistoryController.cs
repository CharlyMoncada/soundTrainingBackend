namespace SoundTraining.WebApi.Controllers
{
    using Domain;
    using Provider.Dao.Interfaces;
    using Provider.Services.Interfaces;
    using System;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Cors;

    public class HistoryController : BaseApiController
    {
        private readonly IDaoService daoService;

        public HistoryController(
            ISessionFactory sessionFactory,
            IDaoService daoSrv)
            : base(sessionFactory)
        {
            daoService = daoSrv;
        }

        [HttpGet]
        [Route("api/history", Name = "History")]
        public IHttpActionResult Get()
        {
            try
            {
                return Content(HttpStatusCode.OK, CatchException(() => daoService.GetAllHistory()));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost, Route("api/history", Name = "SaveHistory")]
        public IHttpActionResult SaveHistory(History history)
        {
            IHttpActionResult result;
            try
            {
                if (CatchException(() => daoService.InsertHistory(history)))
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                result = InternalServerError();
            }

            return result;
        }

        [HttpGet, Route("api/history/users/{id}", Name = "GetByUserID")]
        public IHttpActionResult GetByUserID(int id)
        {
            IHttpActionResult result;
            try
            {
                return Ok(CatchException(() => daoService.GetAllHistoryByUserID(id)));
            }
            catch (Exception ex)
            {
                result = InternalServerError();
            }

            return result;
        }

        [HttpGet, Route("api/history/users/frequencies/{id}", Name = "GetFrequencyByUser"),
        EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetFrequencyByUser(int id)
        {
            IHttpActionResult result;
            try
            {
                return Ok(CatchException(() => daoService.GetFrequencyByUser(id)));
            }
            catch (Exception ex)
            {
                result = InternalServerError();
            }

            return result;
        }

        [HttpGet, Route("api/history/users/frequencies/", Name = "GetFrequencyCount"),
        EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetFrequencyCount()
        {
            IHttpActionResult result;
            try
            {
                return Ok(CatchException(() => daoService.GetFrequencyCount()));
            }
            catch (Exception ex)
            {
                result = InternalServerError();
            }

            return result;
        }
    }
}