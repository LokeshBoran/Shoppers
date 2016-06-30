using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Shoppers.Core.Data;

namespace Shoppers.Pricing.Models
{
    public class ProductPricing : CoreEntity
    {
        [Required]
        public Int64 ProductId { get; set; }


        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Provider { get; set; }

        [Required]
        [Range(0,double.MaxValue)]
        public double Price { get; set; }
        
    }
}
