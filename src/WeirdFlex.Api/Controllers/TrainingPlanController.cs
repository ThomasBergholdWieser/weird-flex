using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Business.Views.Responses;
using WeirdFlex.Business.Views.ViewModels;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Route("api/trainingPlans")]
    public class TrainingPlanController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IRequestDispatcher requestDispatcher;

        public TrainingPlanController(ILogger<TrainingPlanController> logger, IRequestDispatcher requestDispatcher)
        {
            this.logger = logger;
            this.requestDispatcher = requestDispatcher;
        }

        [HttpGet]
        public async Task<IEnumerable<TrainingPlanModel>> Get(long userId, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<TrainingPlan, TrainingPlanModel>(new GetTrainingPlans.Request(userId), cancellationToken);
        }

        [HttpPost]
        public async Task<TrainingPlanModel> Post(CreateTrainingPlanModel model, CancellationToken cancellationToken)
        {
            return await this.requestDispatcher.Dispatch<TrainingPlan, TrainingPlanModel>(new CreateTrainingPlan.Request(model.UserId, model.Name, model.ImageRef), cancellationToken);
        }

        [HttpDelete]
        public async Task Delete(long trainingPlanId, CancellationToken cancellationToken)
        {
            await this.requestDispatcher.Dispatch(new DeleteExercise.Request(trainingPlanId), cancellationToken);
        }

        [HttpPost("{trainingPlanId}/exercises")]
        public async Task AddExerciseToTrainingPlan(long trainingPlanId, [FromBody] ExerciseRef model, CancellationToken cancellationToken)
        {
            await this.requestDispatcher.Dispatch(new AddExerciseToTrainingPlan.Request(1, trainingPlanId, model.Id), cancellationToken);
        }

        [HttpDelete("{trainingPlanId}/exercises")]
        public async Task RemoveExerciseFromTrainingPlan(long trainingPlanId, [FromBody] ExerciseRef model, CancellationToken cancellationToken)
        {
            await this.requestDispatcher.Dispatch(new AddExerciseToTrainingPlan.Request(1, trainingPlanId, model.Id), cancellationToken);
        }
    }
}
