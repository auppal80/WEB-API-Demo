using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.Net.Http;
using System.Web.Http.Routing;

namespace WebAPI.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;

        public ModelFactory(HttpRequestMessage request)
        {
            _urlHelper = new UrlHelper(request);
        
        }
        public CertificateUriModel Create(Certification oc)
        {
            return new CertificateUriModel()
            {
                Certification = oc,
                Url = _urlHelper.Link("Certificate", new { id = oc.CertificationId })
            };

        }

        public UserUriModel CreateUser(User model)
        {
            return new UserUriModel()
            {
                User = model,
                Url = _urlHelper.Link("Users", new { id = model.UserId })
            };
        }

        public AuthTokenModel Create(AuthToken authToken)
        {
            return new AuthTokenModel()
            {
                Token = authToken.Token,
                Expiration = authToken.Expiration
            };
        }
    }
}