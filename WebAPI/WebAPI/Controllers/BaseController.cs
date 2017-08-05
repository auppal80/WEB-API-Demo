using System.Web.Http;
using DataAccess;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        ICertificationRespository _repo;

        ModelFactory _modelFactory;
        public BaseApiController(ICertificationRespository repo)
        {
            _repo = repo;
        }

        protected ICertificationRespository TheRepository
        {
            get
            {
                return _repo;
            }
        }
        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request);
                }
                return _modelFactory;
            }
        }
    }
}
