using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaOnline.Web.Controllers.Base
{
    public abstract class MainController : Controller
    {
        protected IActionResult CustomResponse(dynamic result)
        {
            if (!result.IsValid()) {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "Mensagens", result.GetErrors() }
                }));
            }

            return Ok(result);
        }
    }
}