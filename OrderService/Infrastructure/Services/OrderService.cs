using Microsoft.Extensions.Logging;
using Order.Contract.Repositories;
using Order.Contract.Services;
using Order.Models;

namespace Infrastructure.Services;

public class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    public async Task<List<OrderModal>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrders();
        var orderList=new List<OrderModal>();
        foreach (var order in orders)
        {
            orderList.Add(new OrderModal()
            {
                Id = order.Id,
                Order_Date = order.Order_Date,
                CustomerId = order.CustomerId,
                CustomerName = order.CustomerName,
                PaymentMethodId = order.PaymentMethodId,
                PaymentName = order.PaymentName,
                ShippingAddress = order.ShippingAddress,
                ShippingMethod = order.ShippingMethod,
                BillAmount = order.BillAmount,
                Order_Status = order.Order_Status,
                
                
            });
        }
        return orderList;
        
    }

    public async Task<OrderModal> GetOrdersByCustomerId(int customerId)
    {
        var order = await _orderRepository.GetOrdersByCustomerId(customerId);
        return new OrderModal()
        {
            Id = order.Id,
            Order_Date = order.Order_Date,
            CustomerId = order.CustomerId,
            CustomerName = order.CustomerName,
            PaymentMethodId = order.PaymentMethodId,
            PaymentName = order.PaymentName,
            ShippingAddress = order.ShippingAddress,
            ShippingMethod = order.ShippingMethod,
            BillAmount = order.BillAmount,
            Order_Status = order.Order_Status,

        };
        
    }

    public async Task<OrderModal> SaveOrder(OrderModal order)
    {
        ApplicationCore.Entities.Order orderE = new ApplicationCore.Entities.Order()
        {
            Id = order.Id,
            Order_Date = order.Order_Date,
            CustomerId = order.CustomerId,
            CustomerName = order.CustomerName,
            PaymentMethodId = order.PaymentMethodId,
            PaymentName = order.PaymentName,
            ShippingAddress = order.ShippingAddress,
            ShippingMethod = order.ShippingMethod,
            BillAmount = order.BillAmount,
            Order_Status = order.Order_Status,
        };
        var savedOrder = await _orderRepository.SaveOrder(orderE);
        return new OrderModal
        {
            Id = savedOrder.Id,
            Order_Date = savedOrder.Order_Date,
            CustomerId = savedOrder.CustomerId,
            CustomerName = savedOrder.CustomerName,
            PaymentMethodId = savedOrder.PaymentMethodId,
            PaymentName = savedOrder.PaymentName,
            ShippingAddress = savedOrder.ShippingAddress,
            ShippingMethod = savedOrder.ShippingMethod,
            BillAmount = savedOrder.BillAmount,
            Order_Status = savedOrder.Order_Status
        };
      
    }

    public async Task<OrderModal> UpdateOrder(OrderModal order)
    {
        var existingOrder = await _orderRepository.GetById(order.Id);
        if (existingOrder == null)
        {
            throw new KeyNotFoundException($"Order with ID {order.Id} not found.");
        }
        ApplicationCore.Entities.Order orderE = new ApplicationCore.Entities.Order()
        {
            Id = order.Id,
            Order_Date = order.Order_Date,
            CustomerId = order.CustomerId,
            CustomerName = order.CustomerName,
            PaymentMethodId = order.PaymentMethodId,
            PaymentName = order.PaymentName,
            ShippingAddress = order.ShippingAddress,
            ShippingMethod = order.ShippingMethod,
            BillAmount = order.BillAmount,
            Order_Status = order.Order_Status,
        };
        var savedOrder = await _orderRepository.SaveOrder(orderE);
        return new OrderModal
        {
            Id = savedOrder.Id,
            Order_Date = savedOrder.Order_Date,
            CustomerId = savedOrder.CustomerId,
            CustomerName = savedOrder.CustomerName,
            PaymentMethodId = savedOrder.PaymentMethodId,
            PaymentName = savedOrder.PaymentName,
            ShippingAddress = savedOrder.ShippingAddress,
            ShippingMethod = savedOrder.ShippingMethod,
            BillAmount = savedOrder.BillAmount,
            Order_Status = savedOrder.Order_Status
        };
    }

    public Task<OrderModal> DeleteOrder(int orderId)
    {
        throw new NotImplementedException();
    }
}