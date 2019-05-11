using Moq;
using Newtonsoft.Json;
using Runpath.Common.HttpProcessor;
using Runpath.Common.HttpProcessor.Interfaces;
using Runpath.Dto.V1;
using Runpath.Services.V1;
using System;
using System.Collections.Generic;
using System.IO;
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
            this._mockRepository = new MockRepository(MockBehavior.Strict);

            this._mockHttpRequestFactory = this._mockRepository.Create<IHttpRequestFactory>();
        }

        public void Dispose()
        {
            this._mockRepository.VerifyAll();
        }

        private AlbumService CreateService()
        {
            return new AlbumService(
                this._mockHttpRequestFactory.Object);
        }

        private List<AlbumViewModel> GetAlbumViewModels(string fileLocation)
        {
            string path = string.Empty;

            var directoryInfo = Directory.GetParent(Environment.CurrentDirectory).Parent;
            if (directoryInfo?.Parent != null) path = $"{directoryInfo.Parent.FullName}{fileLocation}";

            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }

            // Load the file
            var fileData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<AlbumViewModel>>(fileData);
        }


        [Theory]
        [InlineData(1, @"\InputFiles\AlbumsUserId1Data.json", 10)]
        [InlineData(6, @"\InputFiles\AlbumsUserId6Data.json", 10)]
        public async Task GetUserAlbums_StateUnderTest_ExpectedBehavior(int userId, string fileLocation, int expectedResult)
        {
            // Arrange
            List<AlbumViewModel> albumViewModels = GetAlbumViewModels(fileLocation);

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage()
            {
                Content = new JsonContent(albumViewModels)
            };
            _mockHttpRequestFactory.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(httpResponseMessage));

            var unitUnderTest = this.CreateService();

            // Act
            var result = await unitUnderTest.GetUserAlbums(userId);


            // Assert
            Assert.True(result.Count == expectedResult);
        }
    }
}
