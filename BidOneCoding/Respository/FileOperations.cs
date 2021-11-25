using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BidOneCoding.Respository
{
    public class FileOperations : IFileOperations
    {
        private readonly string fileName = "data/people.json";
        private readonly string pathOfJsonFile;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileOperations(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            pathOfJsonFile = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
        }

        public string Read()
        {
            return File.ReadAllText(pathOfJsonFile);
        }

        public void Write(string jsonString)
        {
            File.WriteAllText(pathOfJsonFile, jsonString);
        }
    }
}
