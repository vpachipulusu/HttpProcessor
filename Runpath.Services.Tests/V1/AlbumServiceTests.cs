using Moq;
using Runpath.Common.HttpProcessor;
using Runpath.Common.HttpProcessor.Interfaces;
using Runpath.Dto.V1;
using Runpath.Services.Tests.TestData;
using Runpath.Services.V1;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Runpath.Services.Tests.V1
{
    public class AlbumServiceTests : IDisposable
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IHttpRequestFactory> _mockHttpRequestFactory;


        public AlbumServiceTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockHttpRequestFactory = _mockRepository.Create<IHttpRequestFactory>();
        }

        public void Dispose()
        {
            _mockRepository.VerifyAll();
        }

        private AlbumService CreateService()
        {
            return new AlbumService(_mockHttpRequestFactory.Object);
        }

        private async Task<List<AlbumViewModel>> SetupAlbumsService(int userId, string fileLocation)
        {
            // Arrange
            List<AlbumViewModel> albumViewModels = MockedData.GetAlbumViewModels(fileLocation);
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage()
            {
                Content = new JsonContent(albumViewModels)
            };
            _mockHttpRequestFactory.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(httpResponseMessage));
            var unitUnderTest = CreateService();

            return await unitUnderTest.GetUserAlbums(userId);
        }


        [Theory]
        [InlineData(1, @"\InputFiles\AlbumsUserId1Data.json", 10)]
        [InlineData(6, @"\InputFiles\AlbumsUserId6Data.json", 10)]
        public async Task GetUserAlbums_StateUnderTest_ExpectedBehavior(int userId, string fileLocation, int expectedResult)
        {
            // Act
            var result = await SetupAlbumsService(userId, fileLocation);

            // Assert
            Assert.True(result.Count == expectedResult);
        }

        [Theory]
        [InlineData(18, @"\InputFiles\AlbumsUserId1Data.json", 0)]
        [InlineData(6, @"\InputFiles\AlbumsEmptyData.json", 0)]
        public async Task GetUserAlbums_StateUnderTest_EmptyData_ExpectedBehavior(int userId, string fileLocation, int expectedResult)
        {
            // Act
            var result = await SetupAlbumsService(userId, fileLocation);

            // Assert
            Assert.True(result.Count == expectedResult);
        }


    }
}
