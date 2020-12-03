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
    public class CreateTrainingPlan : IRequestHandler<CreateTrainingPlan.Request, IResult<TrainingPlan>>
    {
        public class Request : IRequest<IResult<TrainingPlan>>
        {
            public long UserId { get; }

            public string Name { get; }

            public string? ImageRef { get; }

            public Request(long userId, string name, string? imageRef)
            {
                this.UserId = userId;
                this.Name = name;
                this.ImageRef = imageRef;
            }
        }

        readonly FlexContext dbContext;

        public CreateTrainingPlan(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IResult<TrainingPlan>> Handle(Request request, CancellationToken cancellationToken)
        {
            var maxOrder = (await this.dbContext.TrainingPlans
                .Where(x => x.UserId == request.UserId)
                .MaxAsync(x => (int?)x.Order)) ?? 0;

            maxOrder += 100;

            var newEntity = new TrainingPlan(request.UserId, request.Name)
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
