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
                CertificationId = oc.CertificationId,
                Name = oc.Name,
                Active = oc.Active,
                InsertedDate = oc.InsertedDate,
                IssuedBy = oc.IssuedBy,
                Url = _urlHelper.Link("Certificate", new { id = oc.CertificationId })
            };

        }

        public Certification Convert(CertificateUriModel model)
        {
            Certification certfication = new Certification();

            certfication.CertificationId = model.CertificationId;
            certfication.Name = model.Name;
            certfication.IssuedBy = model.IssuedBy;
            certfication.InsertedDate = model.InsertedDate;
            certfication.Active = model.Active;

            return certfication;
        }

        public UserUriModel CreateUser(User model)
        {
            return new UserUriModel()
            {
                FullName = model.FullName,
                UserName = model.UserName,
                UserId = model.UserId,
                Url = _urlHelper.Link("Users", new { id = model.UserId })
            };
        }

       public User ParseUser(UserUriModel userUriModel)
        {
            User user = new User();
            user.UserId = userUriModel.UserId;
            user.FullName = userUriModel.FullName;
            user.UserName = userUriModel.UserName;
            return user;
        }
        public AuthTokenModel Create(AuthToken authToken)
        {
            return new AuthTokenModel()
            {
                Token = authToken.Token,
                Expiration = authToken.Expiration
            };
        }

        public CertificateUserUriModel GetCertificateInfo(UsersCertification userCertification)
        {
            CertificateUserUriModel certificateUserUriModel = new CertificateUserUriModel();
            certificateUserUriModel.CertificationName = userCertification.Certification.Name;
            certificateUserUriModel.CertificationId = userCertification.CertificationId;
            certificateUserUriModel.CertificationDate = userCertification.CertificationDate;
            certificateUserUriModel.UserId = userCertification.UserId;
            return certificateUserUriModel;
        }
    }
}