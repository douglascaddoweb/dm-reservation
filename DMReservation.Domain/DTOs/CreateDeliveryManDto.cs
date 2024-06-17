namespace DMReservation.Domain.DTOs
{
    public record CreateDeliveryManDto(
        string name,
        string cnpj, 
        DateTime birthdate,
        string cnh,
        string typecnh
        );
    
}
