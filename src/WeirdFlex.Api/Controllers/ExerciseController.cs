using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Business.Views.Responses;
using WeirdFlex.Business.Views.ViewModels;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Authorize(Policy = KnownPolicies.Flexer)]
    [Route("api/exercises")]
    public class ExerciseController : ControllerBase
    {
        private readonly IRequestDispatcher requestDispatcher;

        public ExerciseController(IRequestDispatcher requestDispatcher)
        {
            this.requestDispatcher = requestDispatcher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExerciseModel>))]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<Exercise, ExerciseModel>(new GetExercises.Request(), cancellationToken);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExerciseModel))]
        public async Task<IActionResult> Post(CreateExerciseModel model, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<Exercise, ExerciseModel>(new CreateExercise.Request(model.ExerciseType, model.Name, model.ImageRef), cancellationToken);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Delete(long exerciseId, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch(new DeleteExercise.Request(exerciseId), cancellationToken);
        }
    }
}
