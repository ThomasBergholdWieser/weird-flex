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
    public class RemoveExerciseFromTrainingPlan : IRequestHandler<RemoveExerciseFromTrainingPlan.Request, IResult>
    {
        public class Request : IRequest<IResult<TrainingPlanExercise>>
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

        public RemoveExerciseFromTrainingPlan(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IResult> Handle(Request request, CancellationToken cancellationToken)
        {
            if (!await this.dbContext.TrainingPlans
                .Where(x => x.Id == request.TrainingPlanId && x.UserId == request.UserId)
                .AnyAsync(cancellationToken))
            {
                return Result.Failure<TrainingPlanExercise>("Not allowed");
            }

            var entities = await this.dbContext.TrainingPlanExercises
                .Where(x => x.TrainingPlanId == request.TrainingPlanId && x.ExerciseId == request.ExerciseId)
                .ToListAsync();

            this.dbContext.TrainingPlanExercises
                .RemoveRange(entities);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
