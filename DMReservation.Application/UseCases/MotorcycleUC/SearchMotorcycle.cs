using AutoMapper;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;

namespace DMReservation.Application.UseCases.MotorcycleUC
{
    public class SearchMotorcycle : ISearchMotorcycle
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMapper _mapper;

        public SearchMotorcycle(IMotorcycleRepository motorcycleRepository, IMapper mapper)
        {
            _motorcycleRepository = motorcycleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Realiza a busca por motocicletas cadastradas podendo filtrar pela placa
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        public async Task<List<ListMotorcycleDto>> ExecuteAsync(string plate) 
        {
            try
            {
                List<Motorcycle> motors = await _motorcycleRepository.GetAllWithLicensePlateAsync(plate);

                return _mapper.Map<List<ListMotorcycleDto>>(motors);
            } 
            catch (AutoMapperMappingException ex) { 
                throw new ApplicationBaseException(ex.Message, "MAP"); 
            }
            catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNSEMT");
            }

        }
    }
}
