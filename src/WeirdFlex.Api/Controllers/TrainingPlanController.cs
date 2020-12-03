using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business.Views.Responses;
using WeirdFlex.Business.Views.ViewModels;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainingPlanController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public TrainingPlanController(ILogger<TrainingPlanController> logger, IMediator mediator, IMapper mapper)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TrainingPlanModel>> Get(long userId)
        {
            var result = await this.mediator.Send(new GetTrainingPlans.Request(userId));

            var mapped = this.mapper.Map<IEnumerable<TrainingPlanModel>>(result.Value);

            return mapped;
        }

        [HttpPost]
        public async Task<TrainingPlanModel> Post(CreateTrainingPlanModel model)
        {
            var result = await this.mediator.Send(new CreateTrainingPlan.Request(model.UserId, model.Name, model.ImageRef));

            var mapped = this.mapper.Map<TrainingPlanModel>(result.Value);

            return mapped;
        }
    }
}
