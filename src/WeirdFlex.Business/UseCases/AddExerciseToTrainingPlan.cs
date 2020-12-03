using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class AddExerciseToTrainingPlan : IRequestHandler<AddExerciseToTrainingPlan.Request, Result<TrainingPlanExercise>>
    {
        public class Request : IRequest<Result<TrainingPlanExercise>>
        {
            public long UserId { get; set; }
            public long TrainingPlanId { get; }
            public long ExerciseId { get; }

            public Request(long userId, long trainingPlanId, long exerciseId)
            {
                this.UserId = userId;
                this.TrainingPlanId = trainingPlanId;
                this.ExerciseId = exerciseId;
            }
        }

        readonly FlexContext dbContext;

        public AddExerciseToTrainingPlan(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<TrainingPlanExercise>> Handle(Request request, CancellationToken cancellationToken)
        {
            if (!await this.dbContext.TrainingPlans
                .Where(x => x.Id == request.TrainingPlanId && x.UserId == request.UserId)
                .AnyAsync(cancellationToken))
            {
                return Result.Failure<TrainingPlanExercise>("Not allowed");
            }

            var maxOrder = (await this.dbContext.TrainingPlanExercises
                .Where(x => x.TrainingPlanId == request.TrainingPlanId)
                .MaxAsync(x => (int?)x.Order)) ?? 0;

            maxOrder += 100;

            var newEntity = new TrainingPlanExercise()
            {
                TrainingPlanId = request.TrainingPlanId,
                ExerciseId = request.ExerciseId,
                Order = maxOrder,
            };

            this.dbContext.TrainingPlanExercises
                .Add(newEntity);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return newEntity;
        }
    }
}
