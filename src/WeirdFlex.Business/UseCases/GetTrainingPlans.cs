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
    public class GetTrainingPlans : IRequestHandler<GetTrainingPlans.Request, Result<IList<TrainingPlan>>>
    {
        public class Request : IRequest<Result<IList<TrainingPlan>>>
        {
            public Request()
            {
            }
        }

        readonly FlexContext dbContext;
        readonly IUserContext userContext;

        public GetTrainingPlans(IUserContext userContext, FlexContext dbContext)
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }

        public async Task<Result<IList<TrainingPlan>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await userContext.EnsureUser(cancellationToken);
            if (user == null)
            {
                return Result.Failure<IList<TrainingPlan>>("No user found");
            }

            IList<TrainingPlan> list = await this.dbContext.TrainingPlans
                .Where(x => x.UserId == user.Id)
                .ToListAsync(cancellationToken);

            return Result.Success(list);
        }
    }
}
