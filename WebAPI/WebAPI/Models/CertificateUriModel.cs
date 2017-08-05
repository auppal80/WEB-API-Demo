using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CertificateUriModel
    {
        public Certification Certification { get; set; }
        public string Url { get; set; }
    }
}