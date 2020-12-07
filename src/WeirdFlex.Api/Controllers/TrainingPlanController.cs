using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Policy = KnownPolicies.Flexer)]
    [Route("api/trainingPlans")]
    public class TrainingPlanController : ControllerBase
    {
        private readonly IRequestDispatcher requestDispatcher;

        public TrainingPlanController(IRequestDispatcher requestDispatcher)
        {
            this.requestDispatcher = requestDispatcher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TrainingPlanModel>))]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<TrainingPlan, TrainingPlanModel>(new GetTrainingPlans.Request(), cancellationToken);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainingPlanModel))]
        public async Task<IActionResult> Post(CreateTrainingPlanModel model, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<TrainingPlan, TrainingPlanModel>(new CreateTrainingPlan.Request(model.Name, model.ImageRef), cancellationToken);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Delete(long trainingPlanId, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch(new DeleteExercise.Request(trainingPlanId), cancellationToken);
        }

        [HttpGet("{trainingPlanId}/exercises")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExerciseModel>))]
        public async Task<IActionResult> GetTrainingPlanExercises(long trainingPlanId, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<Exercise, ExerciseModel>(new GetTrainingPlanExercises.Request(trainingPlanId), cancellationToken);
        }

        [HttpPost("{trainingPlanId}/exercises")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> AddExerciseToTrainingPlan(long trainingPlanId, [FromBody] ExerciseRef model, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch(new AddExerciseToTrainingPlan.Request(trainingPlanId, model.Id), cancellationToken);
        }

        [HttpDelete("{trainingPlanId}/exercises")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> RemoveExerciseFromTrainingPlan(long trainingPlanId, [FromBody] ExerciseRef model, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch(new AddExerciseToTrainingPlan.Request(trainingPlanId, model.Id), cancellationToken);
        }
    }
}
