using AutoMapper;
using DMReservation.Application.Interfaces.UseCases.DeliveryManUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using DMReservation.Domain.ValueObjects;

namespace DMReservation.Application.UseCases.DeliveryManUC
{
    public class CreateDeliveryMan : ICreateDeliveryMan
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IMapper _mapper;

        public CreateDeliveryMan(IDeliveryManRepository deliveryManRepository, IMapper mapper)
        {
            _deliveryManRepository = deliveryManRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um cadastro de entregador
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DeliveryManDto> ExecuteAsync(CreateDeliveryManDto model)
        {
            try
            {

                TypeCnh typeCnh = new TypeCnh(model.typecnh);
                Cnpj cnpj = new Cnpj(model.cnpj);
                Cnh cnh = new Cnh(model.cnh);

                if (!typeCnh.IsValid)
                    throw new ApplicationBaseException(MessageSetting.TypeCnhInvalid, "CRDE01");

                if (await GetDeliveryManWithCnpjAsync(cnpj))
                    throw new ApplicationBaseException(MessageSetting.CnpjExist, "CRDE02");

                if (await GetDeliveryManWithCnhAsync(cnh))
                    throw new ApplicationBaseException(MessageSetting.CnhExist, "CRDE03");

                DeliveryMan delivery = new DeliveryMan(model.name, cnpj, model.birthdate, cnh, typeCnh);

                await _deliveryManRepository.AddAsync(delivery);
                await _deliveryManRepository.CommitAsync();
                return _mapper.Map<DeliveryManDto>(delivery);
            }
            catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNCRDE");
            }
        }

        public async Task<bool> GetDeliveryManWithCnpjAsync(Cnpj cnpj)
        {
            if (!cnpj.IsValid) throw new ApplicationBaseException(MessageSetting.CnpjInvalid, "CRDE04");

            return (await _deliveryManRepository.GetDeliveryManWithCnpjAsync(cnpj)) != null;
        }

        public async Task<bool> GetDeliveryManWithCnhAsync(Cnh cnh)
        {
            if (!cnh.IsValid) throw new ApplicationBaseException(MessageSetting.CnhInvalid, "CRDE05");

            return (await _deliveryManRepository.GetDeliveryManWithCnhAsync(cnh)) != null;
        }


    }
}
