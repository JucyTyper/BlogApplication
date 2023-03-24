using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;

namespace BlogApplication.Services
{
    public class FileService : IFileService
    {
        private readonly blogAppDatabase _db;
        ResponseModel response = new ResponseModel();
        public FileService(blogAppDatabase _db)
        {
            this._db = _db;

        }
        public object UploadImage(string id, fileUpload imageFile)
        {
            try
            {
                var Id = new Guid(id);
                string ImageName = id + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_xtx_" + imageFile.file.FileName;
                var folderName = "Assets//Images//ProfileImages";
                var path = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fullpath = path + "//" + ImageName;
                var filestream = File.Create(fullpath);
                imageFile.file.CopyTo(filestream);
                filestream.Close();
                var _user = _db.users.Where(x => (x.UserId == Id)).Select(x => x);
                if (_user.Count() == 0)
                {
                    response.StatusCode = 400;
                    response.Message = "User Not Found";
                    return response;
                }
                _user.First().ProfileImagePath = folderName + ImageName;
                _db.SaveChanges();
                response.Message = "image successfullu uploaded";
                response.IsSuccess = true;
                response.Data = folderName + ImageName;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.IsSuccess = false;
                return response;
            }
        }
    }
}
