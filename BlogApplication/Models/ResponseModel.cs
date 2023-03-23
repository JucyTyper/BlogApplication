namespace BlogApplication.Models
{
    public class ResponseModel
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "Success";
        public object? Data { get; set; } = null;
        public bool IsSuccess { get; set; } = true;
    }
}
