using Ninject;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web;
using DataAccess;
namespace WebAPI.Filters
{
    public class ValidateTokenAttribute : AuthorizationFilterAttribute
    {
        [Inject]
        public CertificationsRepository TheRepository { get; set; }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            const string APIKEYNAME = "apikey";
            const string TOKENNAME = "token";

            var query = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);

            if (!string.IsNullOrWhiteSpace(query[APIKEYNAME]) &&
              !string.IsNullOrWhiteSpace(query[TOKENNAME]))
            {

                var apikey = query[APIKEYNAME];
                var token = query[TOKENNAME];

                var authToken = TheRepository.GetAuthToken(token);

                if (authToken != null && authToken.ApiUser.AppId == apikey && authToken.Expiration > DateTime.UtcNow)
                {
                    return;
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}