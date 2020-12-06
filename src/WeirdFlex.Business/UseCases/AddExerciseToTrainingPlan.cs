using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class AddExerciseToTrainingPlan : IRequestHandler<AddExerciseToTrainingPlan.Request, IResult>
    {
        public class Request : IRequest<IResult>
        {
            public long TrainingPlanId { get; }
            public long ExerciseId { get; }

            public Request(long trainingPlanId, long exerciseId)
            {
                this.TrainingPlanId = trainingPlanId;
                this.ExerciseId = exerciseId;
            }
        }

        readonly FlexContext dbContext;
        readonly IUserContext userContext;

        public AddExerciseToTrainingPlan(IUserContext userContext, FlexContext dbContext)
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }

        public async Task<IResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await userContext.EnsureUser(cancellationToken);
            if (user == null)
            {
                return Result.Failure<TrainingPlanExercise>("No user found");
            }

            if (!await this.dbContext.TrainingPlans
                .Where(x => x.Id == request.TrainingPlanId && x.UserId == user.Id)
                .AnyAsync(cancellationToken))
            {
                return Result.Failure<TrainingPlanExercise>("Not allowed");
            }

            if (await this.dbContext.TrainingPlanExercises
                .Where(x => x.Id == request.TrainingPlanId && x.ExerciseId == request.ExerciseId)
                .AnyAsync(cancellationToken))
            {
                return Result.Failure<TrainingPlanExercise>("Already added");
            }

            var maxOrder = (await this.dbContext.TrainingPlanExercises
                .Where(x => x.TrainingPlanId == request.TrainingPlanId)
                .MaxAsync(x => (int?)x.Order, cancellationToken)) ?? 0;

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

            return Result.Success();
        }
    }
}
