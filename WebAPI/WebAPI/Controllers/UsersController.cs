using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using System.Web.Http.Cors;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [EnableCors("*","*","*")]
    public class UsersController : BaseApiController
    {
        public UsersController(ICertificationRespository repo, IGetCurrentUserIdentity getCurrentUserIdentityService) : base(repo)
        {
        }

        public IHttpActionResult Get()
        {
            return Ok(TheRepository.GetAllUsers());
        }
        public IHttpActionResult Get(int id)
        {
            return Ok(TheRepository.GetUserByUserId(id));
        }

        public IHttpActionResult Post([FromBody] User model)
        {
            if (TheRepository.GetUserByUserName(model.UserName) != null)
            {
                return Conflict();
            }
            if (TheRepository.InsertUser(model) && TheRepository.SaveAll())
            {
                return Created(TheModelFactory.CreateUser(model).Url, model);
            }

            return BadRequest();
        }

        public IHttpActionResult Delete(int id)
        {
            var entity = TheRepository.GetUserByUserId(id);
            if (entity == null)
            {
                return NotFound();
            }

            if (TheRepository.DeleteUser(entity.UserId) && TheRepository.SaveAll())
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}