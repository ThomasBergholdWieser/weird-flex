using WeirdFlex.Common.Enums;

namespace WeirdFlex.Business.Views.Responses
{
    public class ExerciseModel
    {
        public long? Id { get; set; }

        public ExerciseType? ExerciseType { get; set; }

        public string? Name { get; set; }

        public string? ImageRef { get; set; }
    }
}
