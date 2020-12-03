using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Common.Enums;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class DeleteTrainingPlan : IRequestHandler<DeleteTrainingPlan.Request, IResult>
    {
        public class Request : IRequest<IResult>
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

        public DeleteTrainingPlan(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var unattached = new TrainingPlan(request.UserId, "unattached") { Id = request.TrainingPlanId };
            var attached = this.dbContext.TrainingPlans
                .Attach(unattached);
            this.dbContext.TrainingPlans
                .Remove(attached.Entity);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
