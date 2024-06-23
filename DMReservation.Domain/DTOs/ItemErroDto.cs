namespace DMReservation.Domain.DTOs
{
    public class ItemErroDto
    {
        public ItemErroDto(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public string Field { get; set; }

        public string Message { get; set; }
    }
}
