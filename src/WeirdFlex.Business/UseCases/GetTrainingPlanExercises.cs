using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class GetTrainingPlanExercises : IRequestHandler<GetTrainingPlanExercises.Request, Result<IList<Exercise>>>
    {
        public class Request : IRequest<Result<IList<Exercise>>>
        {
            public long TrainingPlanId { get; }

            public Request(long trainingPlanId)
            {
                this.TrainingPlanId = trainingPlanId;
            }
        }

        readonly FlexContext dbContext;
        readonly IUserContext userContext;

        public GetTrainingPlanExercises(IUserContext userContext, FlexContext dbContext)
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }

        public async Task<Result<IList<Exercise>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await userContext.EnsureUser(cancellationToken);
            if (user == null)
            {
                return Result.Failure<IList<Exercise>>("No user found");
            }

            if (!await this.dbContext.TrainingPlans
                .Where(x => x.Id == request.TrainingPlanId && x.UserId == user.Id)
                .AnyAsync(cancellationToken))
            {
                return Result.Failure<IList<Exercise>>("Not allowed");
            }

            IList<Exercise> list = await this.dbContext.TrainingPlanExercises
                .Where(x => x.TrainingPlanId == request.TrainingPlanId)
                .Select(x => x.Exercise)
                .ToListAsync(cancellationToken);

            return Result.Success(list);
        }
    }
}
