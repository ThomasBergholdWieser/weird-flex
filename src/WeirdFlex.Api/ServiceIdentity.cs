using WeirdFlex.Common.Interfaces;

namespace WeirdFlex.Api
{
    public class ServiceIdentity : IServiceIdentity
    {
        public string IdentityProvider { get; set; } = null!;

        public string ClientId { get; set; } = null!;

        public string ClientSecret { get; set; } = string.Empty;
    }
}

