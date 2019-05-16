using System;
using System.Collections.Generic;
using System.IO;
using WillisTowersWatson.Dto.ViewModels;

namespace WillisTowersWatson.Services.Tests.TestData
{
    public static class MockedData
    {
        public static string GetFileData(string fileLocation)
        {
            string path = string.Empty;

            var directoryInfo = Directory.GetParent(Environment.CurrentDirectory).Parent;
            if (directoryInfo?.Parent != null) path = $"{directoryInfo.Parent.FullName}{fileLocation}";

            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }

            // Load the file
            var fileData = File.ReadAllText(path);
            return fileData;
        }


        public static List<NonCumulativePoliciesViewModel> GetNonCumulativePoliciesViewModels()
        {
            List<NonCumulativePoliciesViewModel> NonCumulativePoliciesViewModels = new List<NonCumulativePoliciesViewModel>()
            {
                new NonCumulativePoliciesViewModel()
                {
                    Product = "Comp",
                    OriginYear =1992,
                    DevelopmentYear =1992,
                    IncrementalValue = 110
                },
                 new NonCumulativePoliciesViewModel()
                {
                    Product = "Comp",
                    OriginYear =1992,
                    DevelopmentYear =1993,
                    IncrementalValue = 170
                },
                  new NonCumulativePoliciesViewModel()
                {
                    Product = "Comp",
                    OriginYear =1993,
                    DevelopmentYear =1993,
                    IncrementalValue = 200
                },

                new NonCumulativePoliciesViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1990,
                    DevelopmentYear =1990,
                    IncrementalValue = 45.2M
                },
                 new NonCumulativePoliciesViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1990,
                    DevelopmentYear =1991,
                    IncrementalValue = 64.8M
                },
                  new NonCumulativePoliciesViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1990,
                    DevelopmentYear =1993,
                    IncrementalValue = 37
                },

                   new NonCumulativePoliciesViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1991,
                    DevelopmentYear =1991,
                    IncrementalValue = 50
                },
                 new NonCumulativePoliciesViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1991,
                    DevelopmentYear =1992,
                    IncrementalValue = 75
                },
                  new NonCumulativePoliciesViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1991,
                    DevelopmentYear =1993,
                    IncrementalValue = 25
                },

                      new NonCumulativePoliciesViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1992,
                    DevelopmentYear =1992,
                    IncrementalValue = 55
                },
                 new NonCumulativePoliciesViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1992,
                    DevelopmentYear =1993,
                    IncrementalValue = 85
                },
                  new NonCumulativePoliciesViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1993,
                    DevelopmentYear =1993,
                    IncrementalValue = 100
                }
            };

            return NonCumulativePoliciesViewModels;
        }
    }
}
