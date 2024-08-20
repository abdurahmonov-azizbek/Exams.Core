namespace Exams.Domain.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
