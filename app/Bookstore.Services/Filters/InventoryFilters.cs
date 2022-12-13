﻿namespace Bookstore.Services.Filters
{
    public class InventoryFilters
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public int? PublisherId { get; set; }

        public int? GenreId { get; set; }

        public int? BookTypeId { get; set; }

        public int? ConditionId { get; set; }
    }
}