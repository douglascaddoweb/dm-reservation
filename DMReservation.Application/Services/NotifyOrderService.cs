using AutoMapper;
using DMReservation.Application.Interfaces.Services;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;

namespace DMReservation.Application.Services
{
    public class NotifyOrderService : INotifyOrderService
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
            try
            {
                List<NotifyOrder> notifyOrders = await _notifyOrderRepository.GetAllWithRelationshipsToOrderAsync(idorder);

                return _mapper.Map<List<NotifyOrderDto>>(notifyOrders);
            }
            catch(Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNNTOR");
            }
        }
    }
}
