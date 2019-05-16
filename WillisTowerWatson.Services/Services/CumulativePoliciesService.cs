using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WillisTowersWatson.Dto.ViewModels;
using WillisTowersWatson.Services.Services.Interfaces;

namespace WillisTowersWatson.Services.Services
{
    public class CumulativePoliciesService : ICumulativePoliciesService
    {
        public CumulativePoliciesResultViewModel GetCumulativePolicies(List<NonCumulativePoliciesViewModel> nonCumulativeViewModels)
        {
            CumulativePoliciesResultViewModel cumulativePoliciesResultViewModel = new CumulativePoliciesResultViewModel();
            List<CumulativePoliciesViewModel> cumulativeViewModels = new List<CumulativePoliciesViewModel>();
            foreach (string product in nonCumulativeViewModels.Select(s => s.Product).Distinct().ToList())
            {
                List<NonCumulativePoliciesViewModel> productSpecificViewModels = nonCumulativeViewModels.Where(x => x.Product.Equals(product)).OrderBy(y => y.OriginYear).ToList();
                int startingYear = nonCumulativeViewModels.Select(x => x.OriginYear).Min();
                int endingYear = nonCumulativeViewModels.Select(x => x.OriginYear).Max();
                decimal cumlativeValue = 0;

                cumulativePoliciesResultViewModel.StartingYear = startingYear;
                cumulativePoliciesResultViewModel.TotalNumberOfYears = (endingYear - startingYear) + 1;
                for (int i = 0; i <= endingYear - startingYear; i++)
                {
                    int originStartYear = startingYear + i;
                    for (int j = 0; j <= endingYear - originStartYear; j++)
                    {
                        int originYear = originStartYear;
                        int developmentYear = originStartYear + j;
                        var item = productSpecificViewModels.FirstOrDefault(x => x.OriginYear == originYear && x.DevelopmentYear == developmentYear);
                        if (item != null)
                        {
                            if (item.OriginYear == item.DevelopmentYear)
                            {
                                cumulativeViewModels.Add(new CumulativePoliciesViewModel()
                                {
                                    Product = product,
                                    CumulativeYear = $"{item.OriginYear}-{item.DevelopmentYear}",
                                    Value = item.IncrementalValue
                                });
                                if (item.OriginYear == endingYear)
                                    break;

                                cumlativeValue = item.IncrementalValue;
                                continue;
                            }

                            cumlativeValue = cumlativeValue + item.IncrementalValue;
                            cumulativeViewModels.Add(new CumulativePoliciesViewModel()
                            {
                                Product = product,
                                CumulativeYear = $"{item.OriginYear}-{item.DevelopmentYear}",
                                Value = cumlativeValue
                            });
                        }
                        else
                        {
                            cumulativeViewModels.Add(new CumulativePoliciesViewModel()
                            {
                                Product = product,
                                CumulativeYear = $"{originYear}-{developmentYear}",
                                Value = cumlativeValue
                            });
                        }
                    }
                }

            }
            cumulativePoliciesResultViewModel.CumulativePoliciesViewModels = cumulativeViewModels;
            return cumulativePoliciesResultViewModel;
        }

        public List<NonCumulativePoliciesViewModel> NonCumulativeInputFileReader(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<NonCumulativePoliciesViewModel>();
            }

            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader))
                {
                    return csv.GetRecords<NonCumulativePoliciesViewModel>().ToList();
                }
            }
        }

        public void CumulativeOutputFile(List<NonCumulativePoliciesViewModel> nonCumulativePoliciesViewModels, ref string outputFilePath)
        {
            if (!File.Exists(outputFilePath))
                return;

            CumulativePoliciesResultViewModel cumulativePoliciesResultViewModel = GetCumulativePolicies(nonCumulativePoliciesViewModels);
            var keys = cumulativePoliciesResultViewModel.CumulativePoliciesViewModels.OrderBy(y => y.CumulativeYear).Select(s => s.CumulativeYear).Distinct().ToArray();

            using (var writer = new StreamWriter(outputFilePath))
            {
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteField(cumulativePoliciesResultViewModel.StartingYear);
                    csv.WriteField(cumulativePoliciesResultViewModel.TotalNumberOfYears);
                    csv.NextRecord();

                    foreach (var product in cumulativePoliciesResultViewModel.CumulativePoliciesViewModels.Select(s => s.Product).Distinct().ToList())
                    {
                        csv.WriteField(product);
                        foreach (var key in keys.Distinct())
                        {
                            if (cumulativePoliciesResultViewModel.CumulativePoliciesViewModels.Any(c => c.Product.Equals(product) && c.CumulativeYear.Equals(key)))
                            {
                                foreach (var itemCumulativeViewModel in cumulativePoliciesResultViewModel.CumulativePoliciesViewModels.Where(c => c.Product.Equals(product) && c.CumulativeYear.Equals(key)).OrderBy(y => y.CumulativeYear))
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
        }
    }
}
