using System.Collections.Generic;
using WillisTowersWatson.Dto.ViewModels;

namespace WillisTowersWatson.Services.Services.Interfaces
{
    public interface INonCumulativeCsvFileReader
    {
        List<NonCumulativePoliciesViewModel> NonCumulativeInputFileReader(string filePath);
    }
}
