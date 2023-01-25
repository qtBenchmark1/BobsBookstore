﻿using Bookstore.Domain;
using Bookstore.Domain.Orders;
using Bookstore.Web.Areas.Admin.Models.Orders;
using System.Linq;

namespace Bookstore.Web.Areas.Admin.Mappers.Orders
{
    public static class PaginatedOrderListToOrderIndexViewModelMapper
    {
        public static OrderIndexViewModel ToOrderIndexItemViewModel(this PaginatedList<Order> orders)
        {
            var result = new OrderIndexViewModel();

            foreach (var order in orders)
            {
                result.Items.Add(new OrderIndexListItemViewModel
                {
                    Id = order.Id,
                    CustomerName = order.Customer.FullName,
                    OrderStatus = order.OrderStatus,
                    OrderDate = order.CreatedOn,
                    DeliveryDate = order.DeliveryDate,
                    Total = order.Total
                });
            }

            result.PageIndex = orders.PageIndex;
            result.PageSize = orders.Count;
            result.PageCount = orders.TotalPages;
            result.HasNextPage = orders.HasNextPage;
            result.HasPreviousPage = orders.HasPreviousPage;
            result.PaginationButtons = orders.GetPageList(5).ToList();

            return result;
        }
    }
}
