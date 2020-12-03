using AutoMapper;
using WeirdFlex.Business.Views;
using Xunit;

namespace WeirdFlex.Business.Tests
{
    public class MappingTests
    {
        [Fact]
        public void It_Shall_Be_Valid_Mapping_Configuration()
        {
            // When
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ResponseMappingProfile>());

            // Then
            configuration.AssertConfigurationIsValid();
        }
    }
}
