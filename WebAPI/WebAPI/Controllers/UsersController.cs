using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using System.Web.Http.Cors;
using WebAPI.Services;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    [DisableCors()]
    public class UsersController : BaseApiController
    {
        public UsersController(ICertificationRespository repo, IGetCurrentUserIdentity getCurrentUserIdentityService) : base(repo)
        {
        }
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(TheRepository.GetAllUsers().ToList().Select(x => TheModelFactory.CreateUser(x)).ToList());
        }
        public IHttpActionResult Get(int id)
        {
            return Ok(TheModelFactory.CreateUser(TheRepository.GetUserByUserId(id)));
        }

        public IHttpActionResult Post([FromBody] UserUriModel model)
        {
            if (TheRepository.GetUserByUserName(model.UserName) != null)
            {
                return Conflict();
            }
            if (TheRepository.InsertUser(TheModelFactory.ParseUser(model)) && TheRepository.SaveAll())
            {
                return Created(TheModelFactory.CreateUser(TheModelFactory.ParseUser(model)).Url, model);
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