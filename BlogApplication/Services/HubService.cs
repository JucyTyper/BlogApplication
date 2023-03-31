using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;

namespace BlogApplication.Services
{
    public class HubService :IHubService
    {
        private readonly blogAppDatabase _db;
        // Calling constructor
        public HubService(blogAppDatabase _db)
        {
            this._db = _db;
        }
        // ------------------- A Function to Like and Dislike blogs ------->>
        public ResponseModel likeAndDistlike(string blogId,string userId,int type)
        {
            try
            {
                //Checking if previously liked of disliked blog
                var activity = _db.LikeAndDislikes.Where(x => x.blogId == new Guid(blogId)&&x.userId == new Guid(userId)).Select(x => x);
                //Getting blog Data
                var blog = _db.Blogs.Where(x => x.blogId == new Guid(blogId)&& x.isDeleted == false).Select(x=>x);
                //checking if Blog exist
                if (blog.Count() == 0 )
                {
                    return new ResponseModel(404,"blog Not Found",false);
                }
                //if no previous activity found 
                if (activity.Count() == 0) 
                {
                    //For like
                    if (type == 1) 
                    {
                        // creating a new activity entity with like
                        var activityEntity = new LikeAndDislikeMapModel
                        {
                            userId = new Guid(userId),
                            blogId = new Guid(blogId),
                            isLiked = true
                        };
                        //Saving Data in database
                        _db.LikeAndDislikes.Add(activityEntity);
                        blog!.First().likes = blog!.First().likes + 1;
                        _db.SaveChanges();
                        return new ResponseModel(new TotalLikeDislikeModel(blog.First().blogId,blog!.First().likes, blog!.First().dislikes));
                    }
                    //For Dislike
                    else
                    {
                        // creating a new activity entity for dislike
                        var activityEntity = new LikeAndDislikeMapModel
                        {
                            userId = new Guid(userId),
                            blogId = new Guid(blogId),
                            isDisliked = true
                        };
                        //Saving Data 
                        _db.LikeAndDislikes.Add(activityEntity);
                        blog!.First().dislikes = blog!.First().dislikes + 1;
                        _db.SaveChanges();
                        return new ResponseModel(new TotalLikeDislikeModel(blog.First().blogId, blog!.First().likes, blog!.First().dislikes));
                    }
                }
                //if previous activity exist
                else
                {
                    //for Like
                    if (type == 1)
                    {
                        //if already liked
                        if (activity.First().isLiked == true && activity.First().isDisliked == false)
                        {
                            //Removing Like
                            activity.First().isLiked = false;
                            blog!.First().likes = blog!.First().likes - 1;
                            //saving Changes
                            _db.SaveChanges();
                            return new ResponseModel("blog like removed", new TotalLikeDislikeModel(blog.First().blogId, blog!.First().likes, blog!.First().dislikes));
                        }
                        // If previousl disliked
                        else if (activity.First().isDisliked == true && activity.First().isLiked == false)
                        {
                            //Removing Dislike
                            activity.First().isDisliked = false;
                            //Adding like
                            activity.First().isLiked = true;
                            blog!.First().dislikes -= 1;
                            blog!.First().likes = blog!.First().likes + 1;
                            _db.SaveChanges();
                        }
                        //If on activiy is performed but entity exist
                        else
                        {
                            //Adding Like
                            activity.First().isLiked = true;
                            blog!.First().likes = blog!.First().likes + 1;
                            //Saving Data
                            _db.SaveChanges();
                        }
                        return new ResponseModel(new TotalLikeDislikeModel(blog.First().blogId, blog!.First().likes, blog!.First().dislikes));
                    }
                    else
                    {
                        // If already Disliked 
                        if (activity.First().isDisliked == true && activity.First().isLiked == false)
                        {
                            //removing Dislike
                            activity.First().isDisliked = false;
                            blog!.First().dislikes = blog!.First().dislikes - 1;
                            // Saving Data
                            _db.SaveChanges();
                            return new ResponseModel("blog dislike removed", new TotalLikeDislikeModel(blog.First().blogId, blog!.First().likes, blog!.First().dislikes));
                        }
                        // If previously liked
                        else if (activity.First().isLiked == true && activity.First().isDisliked == false)
                        {
                            // removing Like
                            activity.First().isLiked = false;
                            // Adding Dislike
                            activity.First().isDisliked = true;
                            blog!.First().dislikes += 1;
                            blog!.First().likes = blog.First().likes - 1;
                            // saving Changes
                            _db.SaveChanges();
                        }
                        //If on activiy is performed but entity exist
                        else
                        {
                            //increading Dislike
                            activity.First().isDisliked = true;
                            blog!.First().dislikes = blog!.First().dislikes + 1;
                            // Saving Changes
                            _db.SaveChanges();
                        }
                        return new ResponseModel(new TotalLikeDislikeModel(blog.First().blogId, blog!.First().likes, blog!.First().dislikes));
                    }
                }   
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.StackTrace!, false);
            }
        }
        //---------- A Function to get active notices ---------->>
        public ResponseModel GetNotice()
        {
            try
            {
                //Fetching notices
                var Notices = _db.notices.Where(x => x.CreationTime.AddDays(1) > DateTime.Now  && x.isDeleted == false).Select(x => x).ToList();
                //Checking if notices exist
                if (Notices.Count() == 0)
                {
                    return new ResponseModel(404, "Notice Not found", false);
                }
                return new ResponseModel("Active Notice",Notices);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
    }
}
