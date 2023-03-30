using BlogApplication.Models;
using ChatApp.Models;

namespace BlogApplication.Services
{
    public interface IFileService
    {
        public ResponseModel UploadImage(string id, fileUpload imageFile);
    }
}
