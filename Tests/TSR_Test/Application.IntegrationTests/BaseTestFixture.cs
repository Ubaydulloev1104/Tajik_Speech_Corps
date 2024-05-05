using NUnit.Framework;

namespace Application.IntegrationTests
{
    using static Testing;
    [TestFixture]
    public abstract class BaseTestFixture
    {
        [SetUp]
        public void TestSetUp()
        {
            ResetState();
        }
    }
}
