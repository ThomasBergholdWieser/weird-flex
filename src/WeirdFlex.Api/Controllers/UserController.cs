using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business;
using WeirdFlex.Business.Views.Responses;
using WeirdFlex.Business.Views.ViewModels;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly RequestDispatcher requestDispatcher;

        public UserController(ILogger<UserController> logger, RequestDispatcher requestDispatcher)
        {
            this.logger = logger;
            this.requestDispatcher = requestDispatcher;
        }

        [HttpPost]
        public async Task<UserModel> Post(CreateUserModel model, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<User, UserModel>(new CreateUser.Request(model.DisplayName), cancellationToken);
        }
    }
}
