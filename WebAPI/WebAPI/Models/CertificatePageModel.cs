using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CertificatePageModel
    {
        public List<CertificateUriModel> CertificateUrIModelList { get; set; }
        public string nextPage { get; set; }
        public string prevPage { get; set; }
    }
}