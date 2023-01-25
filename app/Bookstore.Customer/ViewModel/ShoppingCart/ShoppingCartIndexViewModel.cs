﻿using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Web.ViewModel.ShoppingCart
{
    public class ShoppingCartIndexViewModel
    {
        public decimal TotalPrice => ShoppingCartItems.Sum(x => x.Price);

        public List<ShoppingCartIndexItemViewModel> ShoppingCartItems { get; set; } = new List<ShoppingCartIndexItemViewModel>();
    }

    public class ShoppingCartIndexItemViewModel
    {
        public int ShoppingCartItemId { get; set; }

        public long BookId { get; set; }

        public string BookName { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int StockLevel { get; set; }

        public bool HasLowStockLevels { get; set; }

        public bool IsOutOfStock { get; set; }
    }
}