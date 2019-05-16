using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using WillisTowersWatson.Dto.ViewModels;
using WillisTowersWatson.Services.Services;
using WillisTowersWatson.Services.Tests.TestData;
using Xunit;

namespace WillisTowersWatson.Services.Tests.Services
{
    public class CumulativePoliciesServiceTests : IDisposable
    {
        private MockRepository mockRepository;


        public CumulativePoliciesServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private CumulativePoliciesService CreateService()
        {
            return new CumulativePoliciesService();
        }

        [Fact]
        public void GetCumulativePolicies_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            List<NonCumulativePoliciesViewModel> nonCumulativeViewModels = MockedData.GetNonCumulativePoliciesViewModels();

            // Act
            var result = unitUnderTest.GetCumulativePolicies(nonCumulativeViewModels);

            // Assert
            Assert.True(false);
        }

        [Theory]
        [InlineData(@"\InputFiles\InputFile.csv")]
        public void NonCumulativeInputFileReader_StateUnderTest_ExpectedBehavior(string filePath)
        {
            // Arrange
            var unitUnderTest = this.CreateService();

            string path = string.Empty;
            var directoryInfo = Directory.GetParent(Environment.CurrentDirectory).Parent;
            if (directoryInfo?.Parent != null) path = $"{directoryInfo.Parent.FullName}{filePath}";

            // Act
            var result = unitUnderTest.NonCumulativeInputFileReader(path);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void CumulativeOutputFile_StateUnderTest_ExpectedBehavior()
        {
            //// Arrange
            //var unitUnderTest = this.CreateService();
            //List<NonCumulativePoliciesViewModel> nonCumulativePoliciesViewModels = TODO;
            //string outputFilePath = TODO;

            //// Act
            //unitUnderTest.CumulativeOutputFile(
            //    nonCumulativePoliciesViewModels,
            //    ref outputFilePath);

            // Assert
            Assert.True(false);
        }
    }
}
