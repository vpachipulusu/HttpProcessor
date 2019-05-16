using System.Collections.Generic;
using WillisTowersWatson.Dto.ViewModels;

namespace WillisTowersWatson.Services.Services.Interfaces
{
    public interface ICumulativeCsvFileWriter
    {
        void CumulativeOutputFile(List<NonCumulativePoliciesViewModel> nonCumulativePoliciesViewModels, ref string outputFilePath);
    }
}
