using Runpath.Dto.V1;
using Runpath.Services.Tests.TestData;
using Runpath.Services.V1;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Runpath.Services.Tests.V1
{
  public  class CumulativeServiceTests
    {
        [Fact]
        public void GetCumlativeViewModelsTest()
        {
            CumulativeService cumulativeService = new CumulativeService();
            List<CumulativeViewModel> cumulativeViewModels = cumulativeService.GetCumulativeViewModels(MockedData.GetNonCumulativeViewModels());
            Assert.True(cumulativeViewModels.Count > 0);
        }
    }
}
