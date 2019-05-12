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
    }
}
