using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using WebAPI.Filters;
using System.Threading;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [BasicAuthenticationAttribute]
    public class CertifiedUsersController : BaseApiController
    {
        private IGetCurrentUserIdentity _identityService;
        public CertifiedUsersController(ICertificationRespository repo, IGetCurrentUserIdentity getCurrentUserIdentityService) : base(repo)
        {
            _identityService = getCurrentUserIdentityService;
        }

        public IHttpActionResult Get(int userId)
        {
            string name = _identityService.CurrentUser;
            var userModel = TheRepository.GetUserByUserId(userId);
            if (userModel.UserName != name)
                {
                return Unauthorized();
            }
                else
            {
                return Ok(TheRepository.GetCertificationForUser(userId));
            }
         
        }
    }
}
