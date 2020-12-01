using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class GetTrainings : IRequestHandler<GetTrainings.Request, Result<IList<TrainingPlan>>>
    {
        public class Request : IRequest<Result<IList<TrainingPlan>>>
        {
            public long UserId { get; }

            public Request(long userId)
            {
                UserId = userId;
            }
        }

        readonly FlexContext dbContext;

        public GetTrainings(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<IList<TrainingPlan>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var exercises = await this.dbContext.TrainingPlans
                .Where(x )
                .ToListAsync(cancellationToken);

            return exercises;
        }
    }
}
