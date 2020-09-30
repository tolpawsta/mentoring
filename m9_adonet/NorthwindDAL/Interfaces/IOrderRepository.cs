using System;
using System.Collections;
using System.Collections.Generic;
using NorthwindDAL.Entities;
using NorthwindDAL.Enums;

namespace NorthwindDAL.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        Order GetOrder(int id);
        void Create(Order order);
        void Update(Order order);
        void Delete(int id);
        void TransferToWork(Order order, DateTime targetOrderDate);
        void MarkAsDone(Order order, DateTime targetShippedDate);
    }
}