using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CertificateUriModel
    {
        public int CertificationId { get; set; }
        public string Name { get; set; }
        public string IssuedBy { get; set; }
        public System.DateTime InsertedDate { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }
    }
}