using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Services;

namespace TechVault.Infrastructure.Repositories.Services
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;
        public ImageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", src);
           
            if (Directory.Exists(ImageDirectory) is not true)
            {
                Directory.CreateDirectory(ImageDirectory);
            }

            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    var imageName = item.FileName;
                    var imageSrc = Path.Combine(ImageDirectory, imageName);
                    using (FileStream fileStream = new FileStream(imageSrc, FileMode.Create))
                    {
                        await fileStream.CopyToAsync(fileStream);
                    }
                    SaveImageSrc.Add(imageName);
                }

            }
            return SaveImageSrc;

        }

        public void DeleteImageAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);

            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
