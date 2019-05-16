using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [Theory]
        [InlineData("", 20)]
        [InlineData("Comp", 3)]
        [InlineData("Non-Comp", 10)]
        [InlineData("Non Existing-Comp", 0)]
        public void GetCumulativePolicies_StateUnderTest_ExpectedBehavior(string specificProduct, int expectedResult)
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            List<NonCumulativePoliciesViewModel> nonCumulativeViewModels;
            if (string.IsNullOrEmpty(specificProduct))
            {
                nonCumulativeViewModels = MockedData.GetNonCumulativePoliciesViewModels();
            }
            else
            {
                nonCumulativeViewModels = MockedData.GetNonCumulativePoliciesViewModels().Where(p => p.Product.Equals(specificProduct)).ToList();
            }

            // Act
            var result = unitUnderTest.GetCumulativePolicies(nonCumulativeViewModels);

            // Assert
            Assert.True(result.CumulativePoliciesViewModels.Count == expectedResult);
        }


        [Theory]
        [InlineData(@"\TestFiles\InputFile.csv", 12)]
        [InlineData(@"\TestFiles\FileNotExist.csv", 0)]
        public void NonCumulativeInputFileReader_StateUnderTest_ExpectedBehavior(string filePath, int expectedResult)
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            string fullFilePath = GetFullFilePath(filePath);

            // Act
            var result = unitUnderTest.NonCumulativeInputFileReader(fullFilePath);

            // Assert
            Assert.True(result.Count == expectedResult);
        }

        [Theory]
        [InlineData(@"\TestFiles\OutputFile.csv", 95)]
        [InlineData(@"\TestFiles\FileNotExist.csv", 0)]

        public void CumulativeOutputFile_StateUnderTest_ExpectedBehavior(string outputFilePath, int contentLength)
        {
            // Arrange
            var unitUnderTest = this.CreateService();
            outputFilePath = GetFullFilePath(outputFilePath);
            CheckFileExistAndClearContent(outputFilePath);
            List<NonCumulativePoliciesViewModel> nonCumulativePoliciesViewModels = MockedData.GetNonCumulativePoliciesViewModels();

            // Act
            unitUnderTest.CumulativeOutputFile(nonCumulativePoliciesViewModels, ref outputFilePath);
            int content = 0;
            if (File.Exists(outputFilePath))
                content = File.ReadAllText(outputFilePath).Length;

            // Assert
            Assert.True(content == contentLength);
        }
    }
}
