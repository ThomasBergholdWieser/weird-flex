using CSharpFunctionalExtensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WeirdFlex.Business.Interfaces
{
    public interface IRequestDispatcher
    {
        Task<TViewModel> Dispatch<TResponse, TViewModel>(IRequest<Result<TResponse>> request, CancellationToken cancellationToken);
        Task<IEnumerable<TViewModel>> Dispatch<TResponse, TViewModel>(IRequest<Result<IList<TResponse>>> request, CancellationToken cancellationToken);
    }
}