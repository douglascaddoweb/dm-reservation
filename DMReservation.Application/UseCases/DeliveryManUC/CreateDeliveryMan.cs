using DMReservation.Application.Interfaces.UseCases.DeliveryManUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using DMReservation.Domain.ValueObjects;

namespace DMReservation.Application.UseCases.DeliveryManUC
{
    public class CreateDeliveryMan : ICreateDeliveryMan
    {
        private readonly IDeliveryManRepository _deliveryManRepository;

        public CreateDeliveryMan(IDeliveryManRepository deliveryManRepository)
        {
            _deliveryManRepository = deliveryManRepository;
        }

        /// <summary>
        /// Cria um cadastro de entregador
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(CreateDeliveryManDto model)
        {
            try
            {

                TypeCnh typeCnh = new TypeCnh(model.typecnh);
                Cnpj cnpj = new Cnpj(model.cnpj);
                Cnh cnh = new Cnh(model.cnh);

                if (!typeCnh.IsValid)
                    throw new Exception(MessageSetting.TypeCnhInvalid);

                if (await GetDeliveryManWithCnpjAsync(cnpj))
                    throw new Exception(MessageSetting.CnpjExist);

                if (await GetDeliveryManWithCnhAsync(cnh))
                    throw new Exception(MessageSetting.CnhExist);

                DeliveryMan delivery = new DeliveryMan(model.name, cnpj, model.birthdate, cnh, typeCnh);

                await _deliveryManRepository.AddAsync(delivery);
                await _deliveryManRepository.CommitAsync();
            } catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> GetDeliveryManWithCnpjAsync(Cnpj cnpj)
        {
            if (!cnpj.IsValid) throw new Exception(MessageSetting.CnpjInvalid);
            
            return (await _deliveryManRepository.GetDeliveryManWithCnpjAsync(cnpj)) != null;
        }
        
        public async Task<bool> GetDeliveryManWithCnhAsync(Cnh cnh)
        {
            if (!cnh.IsValid) throw new Exception(MessageSetting.CnhInvalid);

            return (await _deliveryManRepository.GetDeliveryManWithCnhAsync(cnh)) != null;
        }
        

    }
}
