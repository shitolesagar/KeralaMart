using System.Collections.Generic;
using System.Threading.Tasks;
using KeralaMiniMart.Entities.WebViewModels;
using Microsoft.AspNetCore.Http;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface IFileServices
    {
        Task<string> SaveImageAndReturnRelativePath(IFormFile file, params string[] folders);
    }
}