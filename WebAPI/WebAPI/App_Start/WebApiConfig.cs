using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Dispatcher;
using WebAPI.HTTPS;
using WebAPI.Filters;
using WebAPI.Services;
namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
               name: "Users",
               routeTemplate: "api/users/{id}",
               defaults: new { controller = "Users", id = RouteParameter.Optional }
           );
            config.Routes.MapHttpRoute(
              name: "CertifiedUsers",
              routeTemplate: "api/CertifiedUsers/{id}",
              defaults: new { controller = "CertifiedUsers" }
          );
            config.Routes.MapHttpRoute(
             name: "DefaultApi",
             routeTemplate: "api/{controller}/{id}",
             defaults: new { id = RouteParameter.Optional }
            );
            config.EnableCors();

            config.Filters.Add(new RequiredHttpsAttribute());
            config.Filters.Add(new ValidateTokenAttribute());
            config.Services.Replace(typeof(IHttpControllerSelector),
            new ControllerSelector(config));
        }
    }
}
