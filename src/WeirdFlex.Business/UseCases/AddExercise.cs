using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Common.Enums;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class AddExercise : IRequestHandler<AddExercise.Request, Result<Exercise>>
    {
        public class Request : IRequest<Result<Exercise>>
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

        public AddExercise(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<Exercise>> Handle(Request request, CancellationToken cancellationToken)
        {
            var newEntity = new Exercise(request.ExerciseType, request.Name)
            {
                ImageRef = request.ImageRef,
            };

            this.dbContext.Exercises
                .Add(newEntity);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return newEntity;
        }
    }
}
