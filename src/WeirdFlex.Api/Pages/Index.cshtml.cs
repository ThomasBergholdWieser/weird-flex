using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Tieto.Lama.PrintApi.Pages;

namespace WeirdFlex.Api.Pages
{
    [AllowAnonymous]
    public class IndexModel : CommonPageModel
    {
        public IndexModel(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        {
        }

        public async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
