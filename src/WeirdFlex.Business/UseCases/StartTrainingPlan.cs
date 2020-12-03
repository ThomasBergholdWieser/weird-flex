﻿using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Common.Enums;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class StartTrainingPlan : IRequestHandler<StartTrainingPlan.Request, Result<TrainingPlanInstance>>
    {
        public class Request : IRequest<Result<TrainingPlanInstance>>
        {
            public long TrainingPlanId { get; set; }

            public DateTime StartedAt { get; set; }

            public Request(long trainingPlanId, DateTime startedAt)
            {
                this.TrainingPlanId = trainingPlanId;
                this.StartedAt = startedAt;
            }
        }

        readonly FlexContext dbContext;

        public StartTrainingPlan(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result<TrainingPlanInstance>> Handle(Request request, CancellationToken cancellationToken)
        {
            var newEntity = new TrainingPlanInstance(request.TrainingPlanId)
            {
                StartedAt = request.StartedAt,
            };

            this.dbContext.TrainingPlanInstances
                .Add(newEntity);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return newEntity;
        }
    }
}
