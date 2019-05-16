using System.Collections.Generic;
using WillisTowersWatson.Dto.ViewModels;

namespace WillisTowersWatson.Services.Services.Interfaces
{
    public interface ICumulativePoliciesService : INonCumulativeCsvFileReader, ICumulativeCsvFileWriter
    {
        CumulativePoliciesResultViewModel GetCumulativePolicies(List<NonCumulativePoliciesViewModel> nonCumulativeViewModels);
    }
}
