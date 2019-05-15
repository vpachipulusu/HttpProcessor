using CsvHelper.Configuration.Attributes;

namespace Runpath.Dto.V1
{
    public class NonCumulativeViewModel
    {
        [Name("Product")]
        public string Product { get; set; }
        [Name("Origin Year")]
        public int OriginYear { get; set; }
        [Name("Development Year")]
        public int DevelopmentYear { get; set; }
        [Name("Incremental Value")]
        public decimal IncrementalValue { get; set; }
    }
}
