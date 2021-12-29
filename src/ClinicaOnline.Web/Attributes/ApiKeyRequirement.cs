using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ClinicaOnline.Web.Attributes
{
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
        public ApiKeyRequirement()
        {
        }
    }
}