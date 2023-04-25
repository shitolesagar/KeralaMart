using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using KeralaMiniMart.Entities.Constant;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction.Service;

namespace KeralaMiniMart.Service
{
    public class FileServices : IFileServices
    {
        public async Task<string> SaveImageAndReturnRelativePath(IFormFile file, params string[] folders)
        {
            string folderPath = CreateImageFolderAndReturnPath(folders);
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(folderPath, filename);
            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return CreateRelativePath(folders, filename);
        }

        private string CreateImageFolderAndReturnPath(string[] folders)
        {
            string uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), FolderConstants.WWWRootFolder, FolderConstants.ImagesFolder);
            foreach (var folder in folders)
            {
                uploadFolderPath = Path.Combine(uploadFolderPath, folder);
                if (!Directory.Exists(uploadFolderPath))
                    Directory.CreateDirectory(uploadFolderPath);
            }
            return uploadFolderPath;
        }

        private string CreateRelativePath(string[] folders,string imageName)
        {
            string path = "/" + FolderConstants.ImagesFolder;
            foreach (var folder in folders)
            {
                path = path + "/" + folder;
            }
            path = path + "/" + imageName;
            return path;
        }
    }
}
