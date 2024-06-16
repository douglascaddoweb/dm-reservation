namespace DMReservation.Application.Interfaces.Services
{
    public interface IMotorcycleService
    {
        Task<bool> GetMotorcycleWithPlate(string licenseplate);
    }
}
