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
    public class GetTrainingPlans : IRequestHandler<GetTrainingPlans.Request, IResult<IList<TrainingPlan>>>
    {
        public class Request : IRequest<IResult<IList<TrainingPlan>>>
        {
            public long UserId { get; }

            public Request(long userId)
            {
                UserId = userId;
            }
        }

        readonly FlexContext dbContext;

        public GetTrainingPlans(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IResult<IList<TrainingPlan>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var list = await this.dbContext.TrainingPlans
                .Where(x => x.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            return Result.Success(list);
        }
    }
}
