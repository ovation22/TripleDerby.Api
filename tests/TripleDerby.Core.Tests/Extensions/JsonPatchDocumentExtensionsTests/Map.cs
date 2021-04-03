using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Moq;
using Newtonsoft.Json.Serialization;
using TripleDerby.Core.Extensions;
using Xunit;

namespace TripleDerby.Core.Tests.Extensions.JsonPatchDocumentExtensionsTests
{
    [Trait("Category", "Patch")]
    public class Map
    {
        [Fact]
        public void ItMapsOperation()
        {
            // Arrange
            var operation = new Operation<In> { value = "value", path = "/path", op = "operation", from = "from" };

            // Act
            var result = operation.Map<Out>();

            // Assert
            Assert.IsType<Operation<Out>>(result);
            Assert.Equal("value", result.value);
            Assert.Equal("/path", result.path);
            Assert.Equal("operation", result.op);
            Assert.Equal("from", result.from);
        }

        [Fact]
        public void ItMapsJsonPatchDocument()
        {
            // Arrange
            var contractResolver = new Mock<IContractResolver>();
            var operations = new List<Operation<In>>();
            var jsonPatchDocument = new JsonPatchDocument<In>(operations, contractResolver.Object);

            // Act
            var result = jsonPatchDocument.Map<In, Out>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonPatchDocument<Out>>(result);
        }

        internal class In
        {
        }

        internal class Out
        {
        }
    }
}
