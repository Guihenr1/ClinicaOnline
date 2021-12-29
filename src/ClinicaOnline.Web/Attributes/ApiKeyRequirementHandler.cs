using System;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ClinicaOnline.Web.Attributes
{
    public class ApiKeyRequirementHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IParceiroService _parceiroService;

        public ApiKeyRequirementHandler(IHttpContextAccessor httpContextAccessor, IParceiroService parceiroService)
        {
            _httpContextAccessor = httpContextAccessor;
            _parceiroService = parceiroService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            var apiKey = _httpContextAccessor.HttpContext.Request.Headers["x-api-key"][0];

            if (apiKey != null && Guid.TryParse(apiKey, out _)){
                if (await _parceiroService.CheckApiKey(Guid.Parse(apiKey))){
                    context.Succeed(requirement);
                } else {
                    context.Fail();
                }
            } else {
                context.Fail();
            }
        }
    }
}