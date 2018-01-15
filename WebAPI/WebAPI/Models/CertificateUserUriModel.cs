using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CertificateUserUriModel
    {
        public int UserId { get; set; }
        public int CertificationId { get; set; }
        public string CertificationName{ get; set; }
        public System.DateTime CertificationDate { get; set; }
    }
}