using System;
namespace AtWork.Models
{
    public enum ResponseStatus
    {
        None = -1,
        Ok = 200,
        Failed = 500,
        Unauthorized = 401
    }

    public class BaseResponse<T>
    {
        public BaseResponse()
        {
        }

        public static BaseResponse<T> OkResponse(T body, Message message)
        {
            return new BaseResponse<T>() { Body = body, Message = message, Result = ResponseStatus.Ok };
        }

        public static BaseResponse<T> OkResponse(T body, string title = "", string description = "", string message = "")
        {
            return new BaseResponse<T>() { Body = body, Message = new Message(title, message, message), Result = ResponseStatus.Ok };
        }

        public static BaseResponse<T> FailedResponse(string title = "", string description = "", string message = "")
        {
            return new BaseResponse<T>() { Message = new Message(title, description, message), Result = ResponseStatus.Failed };
        }

        public T Body { get; set; }
        public Message Message { get; set; }
        public ResponseStatus Result { get; set; }
    }
}
