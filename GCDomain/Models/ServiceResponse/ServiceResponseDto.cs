namespace GCDomain.Models.ServiceResponse
{
    public class ServiceResponseDto<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public string? Token { get; set; } = string.Empty;
    }
}
