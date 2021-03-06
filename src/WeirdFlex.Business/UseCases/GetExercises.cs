﻿using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class GetExercises : IRequestHandler<GetExercises.Request, Result<IList<Exercise>>>
    {
        public class Request : IRequest<Result<IList<Exercise>>>
        {
            public Request()
            {
            }
        }

        readonly FlexContext dbContext;

        public GetExercises(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<IList<Exercise>>> Handle(Request request, CancellationToken cancellationToken)
        {
            IList<Exercise> list = await this.dbContext.Exercises
                .ToListAsync(cancellationToken);

            return Result.Success(list);
        }
    }
}
