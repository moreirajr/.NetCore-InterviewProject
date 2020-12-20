using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Produtos.Infra.CrossCutting.Files
{
    public interface IFileService
    {
        void SaveFile(string fileContent, string folder, string fileName);
        Task SaveFileAsync(IFormFile formFile, string folder, string identifier);
        FileStream ReadFile(string folder, string fileName);
    }
}