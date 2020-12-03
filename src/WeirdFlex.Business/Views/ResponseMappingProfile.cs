using AutoMapper;
using WeirdFlex.Business.Views.Responses;
using WeirdFlex.Data.Model;

namespace WeirdFlex.Business.Views
{
    public class ResponseMappingProfile : Profile
    {
        public ResponseMappingProfile()
        {
            this.CreateMap<Exercise, ExerciseModel>();
            this.CreateMap<TrainingPlan, TrainingPlanModel>();
            this.CreateMap<User, UserModel>();
        }
    }
}
