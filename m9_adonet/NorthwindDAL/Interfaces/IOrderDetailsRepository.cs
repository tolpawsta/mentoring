using System.Collections;
using System.Collections.Generic;
using NorthwindDAL.Entities;

namespace NorthwindDAL.Interfaces
{
    public interface IOrderDetailsRepository
    {
        IEnumerable<OrderDetail> GetOrderDetails(int orderId);
        void Create(IEnumerable<OrderDetail> orderDetails);
        void Update(IEnumerable<OrderDetail> orderDetails);
        void Delete(int orderId);
    }
}