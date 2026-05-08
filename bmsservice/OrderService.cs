using bmslib;
using bmslib.Exceptions;
using bmslib.Resource;
using bmsmodel.Common;
using bmsrepository.Interface;
using bmsservice.Interface;

namespace bmsservice
{
    internal class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        private IOrderRepository _orderRepository = orderRepository;

        public async Task Insert(OrderModel orderModel)
        {
            if (!await _orderRepository.IsExists(orderModel))
            {
                await _orderRepository.Insert(orderModel);
            }
            else
            {
                throw new ValidationException("Order".AlreadyExist());
            }
        }

        public async Task Update(OrderModel orderModel)
        {
            if (!await _orderRepository.IsExists(orderModel))
            {
                await _orderRepository.Update(orderModel);
            }
            else
            {
                throw new ValidationException("Order".AlreadyExist());
            }
        }

        public async Task<OrderModel> GetById(long id)
        {
            return await _orderRepository.GetById(id);
        }

        public async Task<List<OrderModel>> GetAll(DataTableRequestModel dataTableRequestModel)
        {
            OrderModel _orderModel = new();
            if (dataTableRequestModel != null)
            {
                _orderModel = dataTableRequestModel.FilterObj.GetModel<OrderModel>();
                _orderModel.DataTableRequestModel = dataTableRequestModel;
            }
            return await _orderRepository.GetAll(_orderModel);
        }

        public async Task<OrderModel> GetSingle(DataTableRequestModel dataTableRequestModel)
        {
            OrderModel _orderModel = new();
            if (dataTableRequestModel != null)
            {
                _orderModel = dataTableRequestModel.FilterObj.GetModel<OrderModel>();
                _orderModel.DataTableRequestModel = dataTableRequestModel;
            }
            var result = await _orderRepository.GetAll(_orderModel);
            return result.FirstOrDefault();
        }
    }
}
