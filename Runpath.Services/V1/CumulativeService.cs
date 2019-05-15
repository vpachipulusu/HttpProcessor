using Runpath.Dto.V1;
using Runpath.Services.V1.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Runpath.Services.V1
{
    public class CumulativeService : ICumulativeService
    {
        public List<CumulativeViewModel> GetCumulativeViewModels(List<NonCumulativeViewModel> nonCumulativeViewModels)
        {
            List<CumulativeViewModel> cumulativeViewModels = new List<CumulativeViewModel>();
            foreach (string product in nonCumulativeViewModels.Select(s => s.Product).Distinct().ToList())
            {
                List<NonCumulativeViewModel> productSpecificViewModels = nonCumulativeViewModels.Where(x => x.Product.Equals(product)).OrderBy(y => y.OriginYear).ToList();
                int startingYear = nonCumulativeViewModels.Select(x => x.OriginYear).Min();
                int endingYear = nonCumulativeViewModels.Select(x => x.OriginYear).Max();
                decimal cumlativeValue = 0;

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
                                cumulativeViewModels.Add(new CumulativeViewModel()
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
                            cumulativeViewModels.Add(new CumulativeViewModel()
                            {
                                Product = product,
                                CumulativeYear = $"{item.OriginYear}-{item.DevelopmentYear}",
                                Value = cumlativeValue
                            });
                        }
                        else
                        {
                            cumulativeViewModels.Add(new CumulativeViewModel()
                            {
                                Product = product,
                                CumulativeYear = $"{originYear}-{developmentYear}",
                                Value = cumlativeValue
                            });
                        }
                    }
                }

            }
            return cumulativeViewModels;
        }
    }
}
