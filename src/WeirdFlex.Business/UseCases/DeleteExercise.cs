using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Common.Enums;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class DeleteExercise : IRequestHandler<DeleteExercise.Request, IResult>
    {
        public class Request : IRequest<IResult>
        {
            public long ExerciseId { get; }

            public Request(long exerciseId)
            {
                this.ExerciseId = exerciseId;
            }
        }

        readonly FlexContext dbContext;

        public DeleteExercise(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var unattached = new Exercise(ExerciseType.Duration, "unimportant") { Id = request.ExerciseId };
            var attached = this.dbContext.Exercises
                .Attach(unattached);
            this.dbContext.Exercises
                .Remove(attached.Entity);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
