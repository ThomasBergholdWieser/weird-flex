using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Business.Views.Responses;
using WeirdFlex.Business.Views.ViewModels;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IRequestDispatcher requestDispatcher;

        public UserController(ILogger<UserController> logger, IRequestDispatcher requestDispatcher)
        {
            this.logger = logger;
            this.requestDispatcher = requestDispatcher;
        }

        [HttpPost]
        public async Task<UserModel> Post(CreateUserModel model, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<User, UserModel>(new CreateUser.Request(model.DisplayName), cancellationToken);
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
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
