namespace WeirdFlex.Common.Interfaces
{
    public interface IServiceIdentity
    {
        string IdentityProvider { get; }

        string ClientId { get; }

        string ClientSecret { get; }
    }
}
