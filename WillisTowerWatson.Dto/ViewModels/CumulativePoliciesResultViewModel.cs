using System.Collections.Generic;

namespace WillisTowersWatson.Dto.ViewModels
{
    public class CumulativePoliciesResultViewModel
    {
        public List<CumulativePoliciesViewModel> CumulativePoliciesViewModels { get; set; }
        public int StartingYear { get; set; }
        public int TotalNumberOfYears { get; set; }
    }
}
