using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;

namespace BlogApplication.Services
{
    public class FileService : IFileService
    {
        private readonly blogAppDatabase _db;
        public FileService(blogAppDatabase _db)
        {
            this._db = _db;
        }
        public ResponseModel UploadImage(string id, fileUpload imageFile)
        {
            try
            {
                var Id = new Guid(id);
                // creating Name
                string ImageName = id + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_xtx_" + imageFile.file!.FileName;
                // Folder Name
                var folderName = "Assets//Images";
                //Combining folder path and existing code path
                var path = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fullpath = path + "//" + ImageName;
                var filestream = File.Create(fullpath);
                //saving file
                imageFile.file.CopyTo(filestream);
                filestream.Close();
                //for adding general image
                if (imageFile.type== 2)
                {
                    return new ResponseModel("image successfully uploaded", folderName + "//" + ImageName);
                }
                //for adding profile image
                var _user = _db.users.Where(x => (x.UserId == Id)).Select(x => x);
                if (_user.Count() == 0)
                {
                    return new ResponseModel(404, "User NotFound", false);
                }
                // adding image path in user Data
                _user.First().ProfileImagePath = folderName + "//" + ImageName;
                _db.SaveChanges();
                return new ResponseModel( "image successfully uploaded", folderName + "//" + ImageName);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
    }
}
