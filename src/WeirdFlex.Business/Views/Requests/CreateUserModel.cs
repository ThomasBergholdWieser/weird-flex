using WeirdFlex.Common.Enums;

namespace WeirdFlex.Business.Views.ViewModels
{
    public class CreateUserModel
    {
        public string Name { get; set; }

        public CreateUserModel(string name)
        {
            this.Name = name;
        }
    }
}
