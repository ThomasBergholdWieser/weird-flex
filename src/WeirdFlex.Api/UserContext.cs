using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Tieto.Lama.Business.UseCases;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Api
{
    public class UserContext : IUserContext
    {
        readonly IHttpContextAccessor httpContextAccessor;
        readonly FlexContext context;
        readonly IMediator mediator;

        public UserContext(IHttpContextAccessor httpContextAccessor, FlexContext context, IMediator mediator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
            this.mediator = mediator;
        }

        private User? currentUser;

        public async Task<User?> EnsureUser(CancellationToken cancellationToken)
        {
            if(this.currentUser == null)
            {
                await TryResolveUser(cancellationToken);
            }

            if(this.currentUser == null)
            {
                await TryCreateUser(cancellationToken);
            }

            return this.currentUser;
        }

        private async Task TryResolveUser(CancellationToken cancellationToken)
        {
            var uid = this.httpContextAccessor.HttpContext?.User.GetUserUId();
            if (uid != null)
            {
                this.currentUser = await this.context.Users
                    .FirstOrDefaultAsync(x => x.Uid == uid, cancellationToken);
            }
        }

        private async Task TryCreateUser(CancellationToken cancellationToken)
        {
            var uid = this.httpContextAccessor.HttpContext?.User.GetUserUId();
            var displayName = this.httpContextAccessor.HttpContext?.User.GetDisplayName();
            if (uid != null && 
                displayName != null)
            {
                var response = await this.mediator.Send(new CreateUser.Request(displayName, uid), cancellationToken);
                if (response.IsSuccess)
                {
                    this.currentUser = response.Value;
                }
            }
        }
    }
}
