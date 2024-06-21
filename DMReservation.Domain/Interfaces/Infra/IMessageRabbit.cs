namespace DMReservation.Domain.Interfaces.Infra
{
    public interface IMessageRabbit
    {
        Task ExecuteAsync(int idorder, int iddeliveryman);
    }
}
