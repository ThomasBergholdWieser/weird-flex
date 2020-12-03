﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business.Views.Responses;
using WeirdFlex.Business.Views.ViewModels;

namespace WeirdFlex.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ExerciseController(ILogger<ExerciseController> logger, IMediator mediator, IMapper mapper)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ExerciseModel>> Get()
        {
            var result = await this.mediator.Send(new GetExercises.Request());

            var mapped = this.mapper.Map<IEnumerable<ExerciseModel>>(result.Value);

            return mapped;
        }

        [HttpPost]
        public async Task<ExerciseModel> Post(CreateExerciseModel model)
        {
            var result = await this.mediator.Send(new CreateExercise.Request(model.ExerciseType, model.Name, model.ImageRef));

            var mapped = this.mapper.Map<ExerciseModel>(result.Value);

            return mapped;
        }
    }
}
