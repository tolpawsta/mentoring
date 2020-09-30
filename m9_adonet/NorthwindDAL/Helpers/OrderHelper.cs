using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using NorthwindDAL.Enums;

namespace NorthwindDAL.Helpers
{
    public static class OrderHelper
    {
        public static OrderState GetOrderState(DbDataReader reader)
        {

            var orderDate = reader.GetValue("OrderDate") as DateTime?;
            var shippeDate = reader.GetValue("ShippedDate") as DateTime?;
            if (orderDate == null && shippeDate == null)
            {
                return OrderState.New;
            }
            else if (shippeDate == null)
            {
                return OrderState.InShipping;
            }
            return OrderState.Shipped;
        }
    }
}