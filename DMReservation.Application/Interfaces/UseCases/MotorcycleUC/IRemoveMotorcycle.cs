namespace DMReservation.Application.Interfaces.UseCases.MotorcycleUC
{
    public interface IRemoveMotorcycle
    {
        /// <summary>
        /// Exclui uma motocicleta que não tenha locação 
        /// </summary>
        /// <param name="idmotorcycle"></param>
        /// <returns></returns>
        Task ExecuteAsync(int idmotorcycle);
    }
}
