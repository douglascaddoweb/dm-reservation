using AutoMapper;
using DMReservation.Application.Interfaces.Services;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;

namespace DMReservation.Application.Services
{
    public class NotifyOrderService :INotifyOrderService
    {
        private readonly INotifyOrderRepository _notifyOrderRepository;
        private readonly IMapper _mapper;
        public NotifyOrderService(IMapper mapper, INotifyOrderRepository notifyOrderRepository)
        {
            _notifyOrderRepository = notifyOrderRepository;
            _mapper = mapper;
        }

        public async Task<List<NotifyOrderDto>> GetAll(int idorder)
        {
            List<NotifyOrder> notifyOrders = await _notifyOrderRepository.GetAllWithRelationshipsToOrderAsync(idorder);

            return _mapper.Map<List<NotifyOrderDto>>(notifyOrders);

        }
    }
}
