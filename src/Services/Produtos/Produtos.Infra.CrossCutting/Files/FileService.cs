using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Produtos.Infra.CrossCutting.Files
{
    public class FileService : IFileService
    {
        public FileService(string path)
        {
            CurrentPath = string.IsNullOrEmpty(path) ? throw new ArgumentNullException("Caminho inválido") : path;
        }

        public string CurrentPath { get; }

        private void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void SaveFile(string fileContent, string folder, string fileName)
        {
            var path = Path.Combine(CurrentPath, folder);
            CreateDirectoryIfNotExists(path);

            File.WriteAllText($"{path}\\{fileName}", fileContent);
        }

        public async Task SaveFileAsync(IFormFile formFile, string folder, string identifier)
        {
            using (var stream = new FileStream($"{CurrentPath}\\{folder}\\{identifier}_{formFile.FileName}", FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
        }

        public FileStream ReadFile(string folder, string fileName)
        {
            return File.OpenRead($"{CurrentPath}\\{folder}\\{fileName}");
        }
    }
}