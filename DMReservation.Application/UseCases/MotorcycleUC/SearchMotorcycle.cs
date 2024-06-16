using AutoMapper;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;

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

        public async Task<List<ListMotorcycleDto>> ExecuteAsync(string plate) 
        {
            List<Motorcycle> motors = await _motorcycleRepository.GetAllWithLicensePlateAsync(plate);

            return _mapper.Map<List<ListMotorcycleDto>>(motors);

        }
    }
}
