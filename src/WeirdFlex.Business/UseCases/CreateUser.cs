using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Common.Enums;
using WeirdFlex.Data.EF;
using WeirdFlex.Data.Model;

namespace Tieto.Lama.Business.UseCases
{
    public class CreateUser : IRequestHandler<CreateUser.Request, IResult<User>>
    {
        public class Request : IRequest<IResult<User>>
        {
            public string Name { get; }

            public Request(string name)
            {
                this.Name = name;
            }
        }

        readonly FlexContext dbContext;

        public CreateUser(FlexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IResult<User>> Handle(Request request, CancellationToken cancellationToken)
        {
            var newEntity = new User(request.Name);

            this.dbContext.Users
                .Add(newEntity);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(newEntity);
        }
    }
}
