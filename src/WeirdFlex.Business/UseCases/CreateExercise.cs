using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Common.Enums;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class CreateExercise : IRequestHandler<CreateExercise.Request, IResult<Exercise>>
    {
        public class Request : IRequest<IResult<Exercise>>
        {
            public ExerciseType ExerciseType { get; }

            public string Name { get; }

            public string? ImageRef { get; }

            public Request(ExerciseType exerciseType, string name, string? imageRef)
            {
                this.ExerciseType = exerciseType;
                this.Name = name;
                this.ImageRef = imageRef;
            }
        }

        readonly FlexContext dbContext;

        public CreateExercise(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IResult<Exercise>> Handle(Request request, CancellationToken cancellationToken)
        {
            var newEntity = new Exercise(request.ExerciseType, request.Name)
            {
                ImageRef = request.ImageRef,
            };

            this.dbContext.Exercises
                .Add(newEntity);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(newEntity);
        }
    }
}
