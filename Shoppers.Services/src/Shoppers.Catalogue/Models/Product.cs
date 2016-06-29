using Shoppers.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppers.Catalogue.Models
{
    public class Product : CoreEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ProductType { get; set; }
    }
}
