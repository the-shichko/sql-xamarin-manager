namespace SqlManager.ViewModels
{
    public class ResponseResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}