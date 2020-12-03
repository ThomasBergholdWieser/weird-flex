using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Business.Views.Responses;
using WeirdFlex.Business.Views.ViewModels;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IRequestDispatcher requestDispatcher;

        public ExerciseController(ILogger<ExerciseController> logger, IRequestDispatcher requestDispatcher)
        {
            this.logger = logger;
            this.requestDispatcher = requestDispatcher;
        }

        [HttpGet]
        public async Task<IEnumerable<ExerciseModel>> Get(CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<Exercise, ExerciseModel>(new GetExercises.Request(), cancellationToken);
        }

        [HttpPost]
        public async Task<ExerciseModel> Post(CreateExerciseModel model, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<Exercise, ExerciseModel>(new CreateExercise.Request(model.ExerciseType, model.Name, model.ImageRef), cancellationToken);
        }
    }
}
