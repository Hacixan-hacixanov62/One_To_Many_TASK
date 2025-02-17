﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorello_PB101_Demo.Models
{
    public class Product : BaseEntity
    {
        [NotMapped]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
