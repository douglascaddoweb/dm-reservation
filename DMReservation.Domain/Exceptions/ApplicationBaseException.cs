using DMReservation.Domain.DTOs;

namespace DMReservation.Domain.Exceptions
{
    public class ApplicationBaseException : Exception
    {
        public string? Code { get; set; }

        public string? MessageUser { get; set; }

        public int StatusCode { get; set; }

        public List<ItemErroDto> Errors { get; set; }

        public ApplicationBaseException() { }

        public ApplicationBaseException(string message, string code, int statusCode = 400) : base(message)
        {
            Code = code;
            StatusCode = statusCode;
        }

        public ApplicationBaseException(string message, string messageUser, string code, int statusCode = 400, List<ItemErroDto> erros = null) : base(message)
        {
            Code = code;
            MessageUser = messageUser;
            StatusCode = statusCode;
            Errors = erros;
        }

        public ApplicationBaseException(Exception exception, string message, string code, int statusCode = 400) : base(message, exception)
        {
            Code = code;
            StatusCode = statusCode;
        }

        public ApplicationBaseException(Exception exception, string message, string messageUser, string code, int statusCode = 400, List<ItemErroDto> erros = null) : base(message, exception)
        {
            Code = code;
            MessageUser = messageUser;
            StatusCode = statusCode;
            Errors = erros;
        }
    }
}
