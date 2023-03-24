using ChatApp.Models;

namespace BlogApplication.Services
{
    public interface IFileService
    {
        public object UploadImage(string id, fileUpload imageFile);
    }
}
