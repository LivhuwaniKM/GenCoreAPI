using GCDomain.Models.ServiceResponse;

namespace GCDomain.Helpers
{
    public class ResponseHelper : IResponseHelper
    {
        public ServiceResponse<T> CreateResponse<T>(bool isSuccess, int statusCode, string message, T? data)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = isSuccess,
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }

        public ServiceResponseDto<T> CreateResponseWithToken<T>(bool isSuccess, int statusCode, string message, T? data, string token)
        {
            return new ServiceResponseDto<T>
            {
                IsSuccess = isSuccess,
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Token = token
            };
        }
    }

    public interface IResponseHelper
    {
        ServiceResponse<T> CreateResponse<T>(bool isSuccess, int statusCode, string message, T? data);
        ServiceResponseDto<T> CreateResponseWithToken<T>(bool isSuccess, int statusCode, string message, T? data, string token);
    }
}
