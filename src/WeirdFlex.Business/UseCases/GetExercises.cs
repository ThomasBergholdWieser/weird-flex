using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    /// <summary>
    /// Root use case
    /// Handles errors and returns responses accordingly
    /// </summary>
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
            var exercises = await this.dbContext.Exercises
                .ToListAsync(cancellationToken);

            return exercises;
        }
    }
}
