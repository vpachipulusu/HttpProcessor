using System;
using System.Collections.Generic;
using System.Text;

namespace Runpath.Dto.V1
{
   public class CumulativeViewModel
    {
        public string Product { get; set; }
        public decimal Value { get; set; }
        public bool IsSame { get; set; }
    }
}
