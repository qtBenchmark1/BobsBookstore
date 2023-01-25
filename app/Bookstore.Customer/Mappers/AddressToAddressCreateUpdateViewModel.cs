﻿using Bookstore.Domain.Customers;
using Bookstore.Web.ViewModel.Address;

namespace Bookstore.Web.Mappers
{
    public static class AddressToAddressCreateUpdateViewModel
    {
        public static AddressCreateUpdateViewModel ToAddressCreateUpdateViewModel(this Address address)
        {
            return new AddressCreateUpdateViewModel
            {
                Id = address.Id,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                Country = address.Country,
                State = address.State,
                ZipCode = address.ZipCode
            };
        }
    }
}