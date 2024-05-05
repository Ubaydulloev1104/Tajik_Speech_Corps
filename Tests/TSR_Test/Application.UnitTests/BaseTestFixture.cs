using Application.Common.Interfaces;
using Application.Common.Security;
using Application.Common.SlugGeneratorService;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace Application.UnitTests
{
    [TestFixture]
    public abstract class BaseTestFixture
    {
        protected Mock<IApplicationDbContext> _dbContextMock;
        public static IMapper Mapper { get; set; }
        protected Mock<IDateTime> _dateTimeMock;
        protected Mock<ICurrentUserService> _currentUserServiceMock;
        protected Mock<ISlugGeneratorService> _slugGenerator;
        protected Mock<IidentityService> _identityService;

        [SetUp]
        public virtual void Setup()
        {
            _identityService = new Mock<IidentityService>();
            _dbContextMock = new Mock<IApplicationDbContext>();
            _dateTimeMock = new Mock<IDateTime>();
            _slugGenerator = new Mock<ISlugGeneratorService>();
            _dateTimeMock.Setup(x => x.Now).Returns(DateTime.UtcNow);
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(r => r.GetUserId()).Returns(Guid.Empty);
        }
    }

}
