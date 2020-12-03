using WeirdFlex.Common.Enums;

namespace WeirdFlex.Business.Views.ViewModels
{
    public class CreateUserModel
    {
        public string DisplayName { get; set; }

        public CreateUserModel(string displayName)
        {
            this.DisplayName = displayName;
        }
    }
}
