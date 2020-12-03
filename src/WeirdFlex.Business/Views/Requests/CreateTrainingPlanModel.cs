using WeirdFlex.Common.Enums;

namespace WeirdFlex.Business.Views.ViewModels
{
    public class CreateTrainingPlanModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string? ImageRef { get; set; }

        public CreateTrainingPlanModel(long userId, string name, string? imageRef)
        {
            this.UserId = userId;
            this.Name = name;
            this.ImageRef = imageRef;
        }
    }
}
