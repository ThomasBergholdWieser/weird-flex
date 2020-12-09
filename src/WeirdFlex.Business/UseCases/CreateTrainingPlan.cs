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
    public class CreateTrainingPlan : IRequestHandler<CreateTrainingPlan.Request, Result<TrainingPlan>>
    {
        public class Request : IRequest<Result<TrainingPlan>>
        {
            public string Name { get; }

            public string? ImageRef { get; }

            public Request(string name, string? imageRef)
            {
                this.Name = name;
                this.ImageRef = imageRef;
            }
        }

        readonly FlexContext dbContext;
        readonly IUserContext userContext;

        public CreateTrainingPlan(IUserContext userContext, FlexContext dbContext)
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }

        public async Task<Result<TrainingPlan>> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await userContext.EnsureUser(cancellationToken);
            if (user == null)
            {
                return Result.Failure<TrainingPlan>("No user found");
            }

            var maxOrder = (await this.dbContext.TrainingPlans
                .Where(x => x.UserId == user.Id)
                .MaxAsync(x => (int?)x.Order, cancellationToken)) ?? 0;

            maxOrder += 100;

            var newEntity = new TrainingPlan(user.Id, request.Name)
            {
                ImageRef = request.ImageRef,
                Order = maxOrder,
            };

            this.dbContext.TrainingPlans
                .Add(newEntity);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(newEntity);
        }
    }
}
