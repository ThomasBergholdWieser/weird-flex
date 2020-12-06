using System.Threading;
using System.Threading.Tasks;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Business.Interfaces
{
    public interface IUserContext
    {
        Task<User?> EnsureUser(CancellationToken cancellationToken);
    }
}
