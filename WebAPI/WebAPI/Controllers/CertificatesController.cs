using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/Certificates")]
    public class CertificatesController : BaseApiController
    {
        const int PAGE_SIZE = 50;
        public CertificatesController(ICertificationRespository repo, IGetCurrentUserIdentity getCurrentUserIdentityService) : base(repo)
        {
        }
        [Route("", Name = "Certificates")]
        public async Task<IHttpActionResult> Get(int page = 0, int page_Size = PAGE_SIZE)
        {
            var certifications = TheRepository.GetAllCertifications();

            await certifications;

            var orderedCertifications = certifications.Result.OrderBy(c => c.Name);
            var totalCount = orderedCertifications.Count();
            var totalPages = Math.Ceiling((double)totalCount / page_Size);

            var helper = new UrlHelper(Request);

            var certificatePageModel = new CertificatePageModel();

            if (page > 0)
            {
                certificatePageModel.prevPage = helper.Link("Certificates", new { page = page - 1 });
            }

            if (page < totalPages - 1)
            {
                certificatePageModel.nextPage = helper.Link("Certificates", new { page = page + 1 });
            }

            certificatePageModel.CertificateUrIModelList = orderedCertifications.Skip(page_Size * page)
                                                         .Take(page_Size)
                                                         .ToList().Select(oc => TheModelFactory.Create(oc)).ToList();

            return Ok(certificatePageModel);
        }
        [Route("{id}", Name = "Certificate")]
        public IHttpActionResult Get(int id)
        {
            var certfication = TheRepository.GetCertificationbyCertificationId(id);
            if (certfication == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(certfication);
            }
        }

        public IHttpActionResult Post([FromBody] Certification model)
        {
            if (TheRepository.GetCertificationByName(model.Name) != null)
            {
                return Conflict();
            }
            if (TheRepository.InsertCertification(model) && TheRepository.SaveAll())
            {
                return Created(TheModelFactory.Create(model).Url, model);
            }

            return BadRequest();
        }

        [Route("{id}", Name = "Certificate")]
        public IHttpActionResult Delete(int id)
        {
            var entity = TheRepository.GetCertificationbyCertificationId(id);
                if (entity == null)
                {
                    return NotFound();
                }

                if (TheRepository.DeleteCertification(entity.CertificationId) && TheRepository.SaveAll())
                {
                    return Ok();
                }
            return BadRequest();
        }
    }
}