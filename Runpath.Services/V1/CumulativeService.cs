using Runpath.Dto.V1;
using Runpath.Services.V1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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
                int EndingYear = nonCumulativeViewModels.Select(x => x.OriginYear).Max();
                decimal cumlativeValue = 0;
                foreach (var item in productSpecificViewModels)
                {
                    if(item.OriginYear == item.DevelopmentYear)
                    {
                        cumulativeViewModels.Add(new CumulativeViewModel()
                        {
                            Product = product,
                            Value = item.IncrementalValue,
                            IsSame = true
                        });
                        if (item.OriginYear == EndingYear)
                            break;
                      
                        cumlativeValue = item.IncrementalValue;
                        continue;
                    }
                    
                    if (item.OriginYear == startingYear && item.DevelopmentYear == EndingYear)
                    {
                        cumulativeViewModels.Add(new CumulativeViewModel()
                        {
                            Product = product,
                            Value = cumlativeValue,
                            IsSame = false
                        });
                    }
                    cumlativeValue = cumlativeValue + item.IncrementalValue;
                    cumulativeViewModels.Add(new CumulativeViewModel()
                    {
                        Product = product,
                        Value = cumlativeValue,
                        IsSame = false
                    });                   
                }
            }
            return cumulativeViewModels;
        }
    }
}
