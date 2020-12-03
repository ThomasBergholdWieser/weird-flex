using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business.Views.Responses;
using WeirdFlex.Business.Views.ViewModels;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UserController(ILogger<UserController> logger, IMediator mediator, IMapper mapper)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<UserModel> Post(CreateUserModel model)
        {
            var result = await this.mediator.Send(new CreateUser.Request(model.Name));

            var mapped = this.mapper.Map<UserModel>(result.Value);

            return mapped;
        }
    }
}
