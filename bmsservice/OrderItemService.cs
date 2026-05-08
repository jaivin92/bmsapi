using bmslib;
using bmslib.Exceptions;
using bmslib.Resource;
using bmsmodel.Common;
using bmsrepository.Interface;
using bmsservice.Interface;

namespace bmsservice
{
    internal class OrderItemService(IOrderItemRepository orderItemRepository) : IOrderItemService
    {
        private IOrderItemRepository _orderItemRepository = orderItemRepository;

        public async Task Insert(OrderItemModel orderItemModel)
        {
            if (!await _orderItemRepository.IsExists(orderItemModel))
            {
                await _orderItemRepository.Insert(orderItemModel);
            }
            else
            {
                throw new ValidationException("Order Item".AlreadyExist());
            }
        }

        public async Task Update(OrderItemModel orderItemModel)
        {
            if (!await _orderItemRepository.IsExists(orderItemModel))
            {
                await _orderItemRepository.Update(orderItemModel);
            }
            else
            {
                throw new ValidationException("Order Item".AlreadyExist());
            }
        }

        public async Task<OrderItemModel> GetById(long id)
        {
            return await _orderItemRepository.GetById(id);
        }

        public async Task<List<OrderItemModel>> GetAll(DataTableRequestModel dataTableRequestModel)
        {
            OrderItemModel _orderItemModel = new();
            if (dataTableRequestModel != null)
            {
                _orderItemModel = dataTableRequestModel.FilterObj.GetModel<OrderItemModel>();
                _orderItemModel.DataTableRequestModel = dataTableRequestModel;
            }
            return await _orderItemRepository.GetAll(_orderItemModel);
        }

        public async Task<OrderItemModel> GetSingle(DataTableRequestModel dataTableRequestModel)
        {
            OrderItemModel _orderItemModel = new();
            if (dataTableRequestModel != null)
            {
                _orderItemModel = dataTableRequestModel.FilterObj.GetModel<OrderItemModel>();
                _orderItemModel.DataTableRequestModel = dataTableRequestModel;
            }
            var result = await _orderItemRepository.GetAll(_orderItemModel);
            return result.FirstOrDefault();
        }
    }
}
