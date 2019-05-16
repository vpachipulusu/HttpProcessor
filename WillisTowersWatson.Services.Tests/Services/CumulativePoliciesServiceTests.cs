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
        private readonly MockRepository _mockRepository;


        public CumulativePoliciesServiceTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
        }

        public void Dispose()
        {
            this._mockRepository.VerifyAll();
        }

        private CumulativePoliciesService CreateService()
        {
            return new CumulativePoliciesService();
        }

        private string GetFullFilePath(string filePath)
        {
            string fullFilePath = string.Empty;
            var directoryInfo = Directory.GetParent(Environment.CurrentDirectory).Parent;
            if (directoryInfo?.Parent != null) fullFilePath = $"{directoryInfo.Parent.FullName}{filePath}";
            return fullFilePath;
        }

        private void CheckFileExistAndClearContent(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                File.WriteAllText(fileFullPath, string.Empty);
            }
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
            Assert.True(result.CumulativePoliciesViewModels.Count > 0);
        }

        [Theory]
        [InlineData(@"\TestFiles\InputFile.csv")]
        public void NonCumulativeInputFileReader_StateUnderTest_ExpectedBehavior(string filePath)
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            string fullFilePath = GetFullFilePath(filePath);

            // Act
            var result = unitUnderTest.NonCumulativeInputFileReader(fullFilePath);

            // Assert
            Assert.True(result.Count > 0);
        }

        [Theory]
        [InlineData(@"\TestFiles\OutputFile.csv")]

        public void CumulativeOutputFile_StateUnderTest_ExpectedBehavior(string outputFilePath)
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            outputFilePath = GetFullFilePath(outputFilePath);
            CheckFileExistAndClearContent(outputFilePath);
            List<NonCumulativePoliciesViewModel> nonCumulativePoliciesViewModels = MockedData.GetNonCumulativePoliciesViewModels();

            // Act
            unitUnderTest.CumulativeOutputFile(nonCumulativePoliciesViewModels, ref outputFilePath);
            int content = File.ReadAllText(outputFilePath).Length;

            // Assert
            Assert.True(content > 0);
        }
    }
}
