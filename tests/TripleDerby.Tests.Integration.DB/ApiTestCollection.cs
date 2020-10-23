using Xunit;

namespace TripleDerby.Tests.Integration.DB
{
    [CollectionDefinition(CollectionName)]
    public class ApiTestCollection : ICollectionFixture<ApiTestContext>
    {
        public const string CollectionName = "API Test";
    }
}
