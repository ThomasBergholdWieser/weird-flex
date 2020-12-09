using MediatR;

namespace WeirdFlex.Business.Notifications
{
    public class UserAuthenticatedNotification : INotification
    {
        public string DisplayName { get; set; }
        public string Uid { get; set; }

        public UserAuthenticatedNotification(string displayName, string uid)
        {
            this.DisplayName = displayName;
            this.Uid = uid;
        }
    }
}
