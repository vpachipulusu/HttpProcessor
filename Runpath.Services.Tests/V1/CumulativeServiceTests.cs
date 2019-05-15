using CsvHelper;
using Runpath.Dto.V1;
using Runpath.Services.Tests.TestData;
using Runpath.Services.V1;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Runpath.Services.Tests.V1
{
    public class CumulativeServiceTests
    {
        [Fact]
        public void GetCumlativeViewModelsTest()
        {
            CumulativeService cumulativeService = new CumulativeService();
            using (var writer = new StreamWriter(@"C:\\Tests\InputFile.csv"))
            {
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(MockedData.GetNonCumulativeViewModels());
                }
            }
            List<CumulativeViewModel> cumulativeViewModels = cumulativeService.GetCumulativeViewModels(MockedData.GetNonCumulativeViewModels());
            var firstRecord = cumulativeViewModels.First();
            var keys = cumulativeViewModels.OrderBy(y => y.CumulativeYear).Select(s => s.CumulativeYear).Distinct().ToArray();

            var headers = new[] { "ProductName" }.Concat(keys).ToList();

            using (var writer = new StreamWriter(@"C:\\Tests\OutputFile.csv"))
            {
                using (var csv = new CsvWriter(writer))
                {
                    foreach (var header in headers)
                    {
                        csv.WriteField(header);
                    }
                    csv.NextRecord();

                    foreach (var product in cumulativeViewModels.Select(s => s.Product).Distinct().ToList())
                    {
                        csv.WriteField(product);
                        foreach (var key in keys.Distinct())
                        {
                            if (cumulativeViewModels.Any(c => c.Product.Equals(product) && c.CumulativeYear.Equals(key)))
                            {
                                foreach (var itemCumulativeViewModel in cumulativeViewModels.Where(c => c.Product.Equals(product) && c.CumulativeYear.Equals(key)).OrderBy(y => y.CumulativeYear))
                                {
                                    if (itemCumulativeViewModel.CumulativeYear.Equals(key))
                                        csv.WriteField(itemCumulativeViewModel.Value);
                                }
                            }
                            else
                            {
                                csv.WriteField(0);
                            }
                        }
                        csv.NextRecord();
                    }
                }
            }
            Assert.True(cumulativeViewModels.Count > 0);
        }
    }
}
