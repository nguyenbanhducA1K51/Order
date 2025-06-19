using Microsoft.Extensions.Logging;
using Order.Contract.Repositories;
using Order.Contract.Services;
using Order.Models;

namespace Infrastructure.Services;

public class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderService> _logger;
private readonly ICustomerRepository _customerRepository;
    public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
        _customerRepository = customerRepository;
    }
    public async Task<List<OrderDetailsModal>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrders();
        var orderList=new List<OrderDetailsModal>();
        foreach (var order in orders)
        {
            orderList.Add(new OrderDetailsModal()
            {
                Id = order.Id,
                Order_Date = order.Order_Date,
                CustomerId = order.Customer_Id,
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

    public async Task<OrderDetailsModal> GetOrdersByCustomerId(int customerId)
    {
        var order = await _orderRepository.GetOrdersByCustomerId(customerId);
        return new OrderDetailsModal()
        {
            Id = order.Id,
            Order_Date = order.Order_Date,
            CustomerId = order.Customer_Id,
            CustomerName = order.CustomerName,
            PaymentMethodId = order.PaymentMethodId,
            PaymentName = order.PaymentName,
            ShippingAddress = order.ShippingAddress,
            ShippingMethod = order.ShippingMethod,
            BillAmount = order.BillAmount,
            Order_Status = order.Order_Status,

        };
        
    }

    public async Task<OrderDetailsModal> SaveOrder(OrderModal order)
    {
        int customerId = order.CustomerId;
        var customer=await _customerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }
        ApplicationCore.Entities.Order orderE = new ApplicationCore.Entities.Order()
        {
            Order_Date = order.Order_Date,
            Customer_Id = order.CustomerId,
            CustomerName = order.CustomerName,
            PaymentMethodId = order.PaymentMethodId,
            PaymentName = order.PaymentName,
            ShippingAddress = order.ShippingAddress,
            ShippingMethod = order.ShippingMethod,
            BillAmount = order.BillAmount,
            Order_Status = order.Order_Status,
        };
        var savedOrder = await _orderRepository.SaveOrder(orderE);
        return new OrderDetailsModal()
        {
            Id = savedOrder.Id,
            Order_Date = savedOrder.Order_Date,
            CustomerId = savedOrder.Customer_Id,
            CustomerName = savedOrder.CustomerName,
            PaymentMethodId = savedOrder.PaymentMethodId,
            PaymentName = savedOrder.PaymentName,
            ShippingAddress = savedOrder.ShippingAddress,
            ShippingMethod = savedOrder.ShippingMethod,
            BillAmount = savedOrder.BillAmount,
            Order_Status = savedOrder.Order_Status
        };
      
    }

    public async Task<OrderDetailsModal> UpdateOrder(OrderModal order,int orderId)
    {
        var existingOrder = await _orderRepository.GetById(orderId);
        if (existingOrder == null)
        {
            throw new KeyNotFoundException($"Order with ID {orderId} not found.");
        }
        ApplicationCore.Entities.Order orderE = new ApplicationCore.Entities.Order()
        {
            Id = orderId,
            Order_Date = order.Order_Date,
            Customer_Id = order.CustomerId,
            CustomerName = order.CustomerName,
            PaymentMethodId = order.PaymentMethodId,
            PaymentName = order.PaymentName,
            ShippingAddress = order.ShippingAddress,
            ShippingMethod = order.ShippingMethod,
            BillAmount = order.BillAmount,
            Order_Status = order.Order_Status,
        };
        var savedOrder = await _orderRepository.SaveOrder(orderE);
        return new OrderDetailsModal()
        {
            Id = savedOrder.Id,
            Order_Date = savedOrder.Order_Date,
            CustomerId = savedOrder.Customer_Id,
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