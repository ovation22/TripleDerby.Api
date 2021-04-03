using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Services.HorseServiceTests
{
    public class Update : HorseServiceTestBase
    {
        private readonly Guid _id;
        private readonly Horse _horse;
        private readonly JsonPatchDocument<HorsePatch> _patch;

        public Update()
        {
            _id = new Guid("1BF95F24-0C0B-4C9A-8B80-46110CA9413E");
            _horse = new Horse
                {
                    Id = _id,
                    Name = "George"
                };
            _patch = new JsonPatchDocument<HorsePatch>();

            Repository.Setup(x => x.Get(It.IsAny<HorseSpecification>())).ReturnsAsync(_horse);
        }

        [Fact]
        public async Task ItUpdatesHorse()
        {
            // Arrange
            _patch.Replace(x => x.Name, "Sam");

            // Act
            await Service.Update(_id, _patch);

            // Assert
            Assert.Equal("Sam", _horse.Name);
        }
    }
}