﻿namespace Fiorello_PB101_Demo.ViewModels.Products
{
    public class ProductDetialVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
         public List<ProductImageVM>  Images { get; set; }
    }
}
