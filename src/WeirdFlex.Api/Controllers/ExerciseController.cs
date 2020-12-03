using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business.Views.ViewModels;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IMediator mediator;

        public ExerciseController(ILogger<ExerciseController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var result = await this.mediator.Send(new GetExercises.Request());

            return new string[0];
        }

        [HttpPost]
        public async Task Post(CreateExerciseModel model)
        {
            var result = await this.mediator.Send(new CreateExercise.Request(model.ExerciseType, model.Name, model.ImageRef));
        }
    }
}
