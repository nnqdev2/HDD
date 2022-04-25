using Microsoft.Extensions.Options;

namespace HDD.FileUploads
{
    public class FileUploadService : IFileUploadService
    {
        private readonly ILogger _logger;
        private readonly FileUploadOptions _options;

        public FileUploadService(ILoggerFactory loggerFactory, IOptions<FileUploadOptions> options)
        {
            _logger = loggerFactory.CreateLogger<FileUploadService>();
            _options = options.Value;
        }

        public async Task UploadFile(List<IFormFile> files)
        {
            var basePath = _options.BasePath;
            long size = files.Sum(f => f.Length);
            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                    var filePath = Path.Combine(basePath, formFile.FileName);
                    filePaths.Add(filePath);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                }
            }
        }
    }
}
