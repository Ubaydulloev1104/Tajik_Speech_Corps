using Application.Features.Applications;
using Application.Features.WordCategories;
using Application.Features.Wordfd;
using AutoMapper.Internal;

namespace Application.UnitTests
{
    [SetUpFixture]
    public class Testing
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            MapperConfiguration configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.Internal().MethodMappingEnabled = false;
                cfg.AddProfile<WordProfile>();
                cfg.AddProfile<ApplicationProfile>();
                cfg.AddProfile<WordCategoryProfile>();
            });
            BaseTestFixture.Mapper = configurationProvider.CreateMapper();
        }
    }
}
