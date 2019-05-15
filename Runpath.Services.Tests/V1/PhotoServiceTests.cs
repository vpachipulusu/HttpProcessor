using Moq;
using Runpath.Common.HttpProcessor;
using Runpath.Common.HttpProcessor.Interfaces;
using Runpath.Dto.V1;
using Runpath.Services.Tests.TestData;
using Runpath.Services.V1;
using Runpath.Services.V1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Runpath.Services.Tests.V1
{
    public class PhotoServiceTests : IDisposable
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<IHttpRequestFactory> _mockHttpRequestFactory;
        private readonly Mock<IAlbumService> _mockAlbumService;

        public PhotoServiceTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockHttpRequestFactory = _mockRepository.Create<IHttpRequestFactory>();
            _mockAlbumService = _mockRepository.Create<IAlbumService>();
        }

        public void Dispose()
        {
            _mockRepository.VerifyAll();
        }

        private PhotoService CreateService()
        {
            return new PhotoService(_mockHttpRequestFactory.Object, _mockAlbumService.Object);
        }

        private async Task<List<PhotoViewModel>> SetupPhotoViewModels(int userId, string albumsFileLocation, string photosFileLocation)
        {
            // Arrange
            List<AlbumViewModel> albumViewModels = MockedData.GetAlbumViewModels(albumsFileLocation);
            _mockAlbumService.Setup(x => x.GetUserAlbums(userId)).Returns(Task.FromResult(albumViewModels.Where(a => a.UserId == userId).ToList()));
            List<PhotoViewModel> photoViewModels = MockedData.GetPhotoViewModels(photosFileLocation);
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage()
            {
                Content = new JsonContent(photoViewModels)
            };
            _mockHttpRequestFactory.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(httpResponseMessage));
            var unitUnderTest = CreateService();

            return await unitUnderTest.GetUserSpecificAlbumPhotos(userId);
        }

        [Theory]
        [InlineData(1, @"\InputFiles\AlbumsUserId1Data.json", @"\InputFiles\PhotosUserId1Data.json", 500)]
        [InlineData(6, @"\InputFiles\AlbumsUserId6Data.json", @"\InputFiles\PhotosUserId6Data.json", 500)]
        public async Task GetUserSpecificAlbumPhotos_StateUnderTest_ExpectedBehavior(int userId, string albumsFileLocation, string photosFileLocation, int expectedResult)
        {
            // Act
            var result = await SetupPhotoViewModels(userId, albumsFileLocation, photosFileLocation);

            // Assert
            Assert.True(result.Count == expectedResult);
        }

        [Theory]
        [InlineData(1, @"\InputFiles\AlbumsUserId1Data.json", @"\InputFiles\PhotosEmptyData.json", 0)]
        [InlineData(6, @"\InputFiles\AlbumsEmptyData.json", @"\InputFiles\PhotosUserId6Data.json", 0)]
        [InlineData(18, @"\InputFiles\AlbumsUserId6Data.json", @"\InputFiles\PhotosUserId6Data.json", 0)]
        public async Task GetUserSpecificAlbumPhotos_StateUnderTest_EmptyData_ExpectedBehavior(int userId, string albumsFileLocation, string photosFileLocation, int expectedResult)
        {
            // Act
            var result = await SetupPhotoViewModels(userId, albumsFileLocation, photosFileLocation);

            // Assert
            Assert.True(result.Count == expectedResult);
        }
    }
}
