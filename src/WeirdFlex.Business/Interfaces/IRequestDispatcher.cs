using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WeirdFlex.Business.Interfaces
{
    public interface IRequestDispatcher
    {
        Task Dispatch(IRequest<IResult> request, CancellationToken cancellationToken);

        Task<TViewModel> Dispatch<TResponse, TViewModel>(IRequest<IResult<TResponse>> request, CancellationToken cancellationToken);

        Task<IEnumerable<TViewModel>> Dispatch<TResponse, TViewModel>(IRequest<IResult<IList<TResponse>>> request, CancellationToken cancellationToken);
    }
}