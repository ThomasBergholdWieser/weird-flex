using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WeirdFlex.Business.Interfaces
{
    public interface IRequestDispatcher
    {
        Task<IActionResult> Dispatch(IRequest<IResult> request, CancellationToken cancellationToken);

        Task<IActionResult> Dispatch<TResponse, TViewModel>(IRequest<IResult<TResponse>> request, CancellationToken cancellationToken);

        Task<IActionResult> Dispatch<TResponse, TViewModel>(IRequest<IResult<IList<TResponse>>> request, CancellationToken cancellationToken);
    }
}