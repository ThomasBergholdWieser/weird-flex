using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class GetTrainingPlanExercises : IRequestHandler<GetTrainingPlanExercises.Request, IResult<IList<Exercise>>>
    {
        public class Request : IRequest<IResult<IList<Exercise>>>
        {
            public long UserId { get; }

            public long TrainingPlanId { get; }

            public Request(long userId, long trainingPlanId)
            {
                this.UserId = userId;
                this.TrainingPlanId = trainingPlanId;
            }
        }

        readonly FlexContext dbContext;

        public GetTrainingPlanExercises(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IResult<IList<Exercise>>> Handle(Request request, CancellationToken cancellationToken)
        {
            if (!await this.dbContext.TrainingPlans
                .Where(x => x.Id == request.TrainingPlanId && x.UserId == request.UserId)
                .AnyAsync(cancellationToken))
            {
                return Result.Failure<IList<Exercise>>("Not allowed");
            }

            var list = await this.dbContext.TrainingPlanExercises
                .Where(x => x.TrainingPlanId == request.TrainingPlanId)
                .Select(x => x.Exercise)
                .ToListAsync(cancellationToken);

            return Result.Success(list);
        }
    }
}
