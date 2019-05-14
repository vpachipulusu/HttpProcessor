using System;
using System.Collections.Generic;
using System.Text;

namespace Runpath.Dto.V1
{
   public class NonCumulativeViewModel
    {
        public string Product { get; set; }
        public int OriginYear { get; set; }
        public int DevelopmentYear { get; set; }
        public decimal IncrementalValue { get; set; }
    }
}
