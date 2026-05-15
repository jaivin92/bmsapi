using bmslib;
using bmslib.Enmus;
using bmslib.Exceptions;
using bmslib.Resource;
using bmsmodel.Common;
using bmsrepository.Interface;
using bmsservice.Interface;

namespace bmsservice
{
    internal class OrderService(IOrderRepository orderRepository, IFoodTableRepository foodTableRepository) : IOrderService
    {
        private IOrderRepository _orderRepository = orderRepository;
        private IFoodTableRepository _foodTableRepository = foodTableRepository;

        public async Task Insert(OrderModel orderModel)
        {
            await ApplyOrderAndTableRules(orderModel);

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
            await ApplyOrderAndTableRules(orderModel);

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

        private async Task ApplyOrderAndTableRules(OrderModel orderModel)
        {
            await _foodTableRepository.ReleaseCleaningTablesOlderThan(10);

            if (orderModel.OrderType == OrderType.DineIn)
            {
                if (!orderModel.FoodTableId.HasValue || orderModel.FoodTableId <= 0)
                {
                    throw new ValidationException("TableId is required for DineIn orders.");
                }

                if (orderModel.OrderStatus == OrderStatus.Accepted)
                {
                    await _foodTableRepository.UpdateTableStatus(orderModel.FoodTableId.Value, FoodTableType.Occupied);
                }

                if (orderModel.OrderStatus == OrderStatus.Completed)
                {
                    await _foodTableRepository.UpdateTableStatus(orderModel.FoodTableId.Value, FoodTableType.Cleaning);
                }
            }
        }
    }
}
