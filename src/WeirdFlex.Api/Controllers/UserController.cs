using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Business.Views.Responses;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        public IActionResult GetUser()
        {
            var fullName = User.GetDisplayName();
            var acctName = User.GetAccountName();
            var uid = User.GetUserUId();

            var roleClaims = User.Claims
                .Select(x => x.Value)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            return new JsonResult(new 
            {
                DisplayName = fullName,
                AccountName = acctName,
                UId = uid,
                Roles = roleClaims,
            });
        }

        [HttpGet]
        [Route("me/login")]
        [AllowAnonymous]
        public async Task Login()
        {
            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties(new Dictionary<string, string?>())
                {
                    RedirectUri = "/"
                });
        }

        [HttpGet]
        [Route("me/logout")]
        public async Task Logout()
        {
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
