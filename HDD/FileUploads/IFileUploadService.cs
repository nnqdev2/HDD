namespace HDD.FileUploads
{
    public interface IFileUploadService
    {
        Task UploadFile(List<IFormFile> files);
    }
}
