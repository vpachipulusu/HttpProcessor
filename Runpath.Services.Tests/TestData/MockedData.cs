using Newtonsoft.Json;
using Runpath.Dto.V1;
using System;
using System.Collections.Generic;
using System.IO;

namespace Runpath.Services.Tests.TestData
{
    public static class MockedData
    {
        private static string GetFileData(string fileLocation)
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

        public static List<AlbumViewModel> GetAlbumViewModels(string fileLocation)
        {
            string fileData = GetFileData(fileLocation);
            return JsonConvert.DeserializeObject<List<AlbumViewModel>>(fileData);
        }

        public static List<PhotoViewModel> GetPhotoViewModels(string fileLocation)
        {
            string fileData = GetFileData(fileLocation);
            return JsonConvert.DeserializeObject<List<PhotoViewModel>>(fileData);
        }

        public static List<NonCumulativeViewModel> GetNonCumulativeViewModels()
        {
            List<NonCumulativeViewModel> nonCumulativeViewModels = new List<NonCumulativeViewModel>()
            {
                new NonCumulativeViewModel()
                {
                    Product = "Comp",
                    OriginYear =1992,
                    DevelopmentYear =1992,
                    IncrementalValue = 110
                },
                 new NonCumulativeViewModel()
                {
                    Product = "Comp",
                    OriginYear =1992,
                    DevelopmentYear =1993,
                    IncrementalValue = 170
                },
                  new NonCumulativeViewModel()
                {
                    Product = "Comp",
                    OriginYear =1993,
                    DevelopmentYear =1993,
                    IncrementalValue = 200
                },

                new NonCumulativeViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1990,
                    DevelopmentYear =1990,
                    IncrementalValue = 45.2M
                },
                 new NonCumulativeViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1990,
                    DevelopmentYear =1991,
                    IncrementalValue = 64.8M
                },
                  new NonCumulativeViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1990,
                    DevelopmentYear =1993,
                    IncrementalValue = 37
                },

                   new NonCumulativeViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1991,
                    DevelopmentYear =1991,
                    IncrementalValue = 50
                },
                 new NonCumulativeViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1991,
                    DevelopmentYear =1992,
                    IncrementalValue = 75
                },
                  new NonCumulativeViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1991,
                    DevelopmentYear =1993,
                    IncrementalValue = 25
                },

                      new NonCumulativeViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1992,
                    DevelopmentYear =1992,
                    IncrementalValue = 55
                },
                 new NonCumulativeViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1992,
                    DevelopmentYear =1993,
                    IncrementalValue = 85
                },
                  new NonCumulativeViewModel()
                {
                    Product = "Non-Comp",
                    OriginYear =1993,
                    DevelopmentYear =1993,
                    IncrementalValue = 100
                }
            };

            return nonCumulativeViewModels;
        }
    }
}
