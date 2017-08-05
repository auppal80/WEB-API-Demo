using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebAPI.Services
{
    public class GetCurrentUserIdentity: IGetCurrentUserIdentity
    {
        public string CurrentUser
        {
            get
            { return Thread.CurrentPrincipal.Identity.Name; }

        }
    }
}
