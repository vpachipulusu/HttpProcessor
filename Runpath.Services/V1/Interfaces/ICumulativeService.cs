using Runpath.Dto.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runpath.Services.V1.Interfaces
{
    public interface ICumulativeService
    {
        List<CumulativeViewModel> GetCumulativeViewModels(List<NonCumulativeViewModel> nonCumulativeViewModels);
    }
}
