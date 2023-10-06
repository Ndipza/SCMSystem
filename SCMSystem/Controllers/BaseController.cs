using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SCMSystem.Controllers
{
    public class BaseController : ControllerBase
    {
        public string? GetUserId()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}
