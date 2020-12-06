using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Business.Interfaces;

namespace WeirdFlex.Business
{
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public RequestDispatcher(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Dispatch<TResponse, TViewModel>(IRequest<IResult<TResponse>> request, CancellationToken cancellationToken = default)
        {
            var result = await this.mediator.Send(request, cancellationToken);

            if (result.IsFailure)
            {
                return new BadRequestObjectResult(result);
            }

            var value = this.mapper.Map<TViewModel>(result.Value);

            return new OkObjectResult(value);
        }

        public async Task<IActionResult> Dispatch<TResponse, TViewModel>(IRequest<IResult<IList<TResponse>>> request, CancellationToken cancellationToken)
        {
            var result = await this.mediator.Send(request, cancellationToken);

            if (result.IsFailure)
            {
                return new BadRequestObjectResult(result);
            }

            var value = this.mapper.Map<IEnumerable<TViewModel>>(result.Value);

            return new OkObjectResult(value);
        }

        public async Task<IActionResult> Dispatch(IRequest<IResult> request, CancellationToken cancellationToken)
        {
            var result = await this.mediator.Send(request, cancellationToken);

            if (result.IsFailure)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(true);
        }
    }
}
