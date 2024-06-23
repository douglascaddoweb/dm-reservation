using DMReservation.Domain.Exceptions;
using Newtonsoft.Json;

namespace DMReservation.Domain.DTOs
{
    public class ErrorDto
    {
        public string? Code { get; set; }
        public int? Status { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? MessageUser { get; set; }
        public virtual Exception? Exception { get; set; }
        public List<ItemErroDto> Errors { get; set; }

        public ErrorDto() { }

        public ErrorDto(ApplicationBaseException app)
        {
            Code = app.Code;
            Status = app.StatusCode;
            Title = "Bad Request";
            Message = app.Message;
            MessageUser = app.MessageUser;
            Exception = app.InnerException;
            Errors = app.Errors;
        }

        public object GetMessageUser()
        {
            string? msg = !string.IsNullOrWhiteSpace(MessageUser) ? MessageUser : Message;

            return new { Code, Title, Status, Message = msg, Errors };
        }

        public string GetMessageSystem()
        {
            return JsonConvert.SerializeObject(new { Code, Title, Status, Message, MessageUser, Exception });
        }
    }
}
