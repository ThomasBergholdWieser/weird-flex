using System;

namespace WeirdFlex.Business.Views.Responses
{
    public class TrainingPlanModel
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public string? ImageRef { get; set; }

        public DateTime? LastExecution { get; set; }

        public int? Order { get; set; }
    }
}
