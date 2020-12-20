using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Produtos.Infra.CrossCutting.Files
{
    public class FileService : IFileService
    {
        private readonly string _path;
        public FileService(string path)
        {
            _path = string.IsNullOrEmpty(path) ? throw new ArgumentNullException("Caminho inválido") : path;
        }

        public string CurrentPath => _path;

        private void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void SaveFile(string fileContent, string folder, string fileName)
        {
            var path = Path.Combine(_path, folder);
            CreateDirectoryIfNotExists(path);

            File.WriteAllText($"{path}\\{fileName}", fileContent);
        }

        public async Task SaveFileAsync(IFormFile formFile, string folder, string identifier)
        {
            using (var stream = new FileStream($"{_path}\\{folder}\\{identifier}_{formFile.FileName}", FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
        }

        public FileStream ReadFile(string folder, string fileName)
        {
            return File.OpenRead($"{_path}\\{folder}\\{fileName}");
        }
    }
}