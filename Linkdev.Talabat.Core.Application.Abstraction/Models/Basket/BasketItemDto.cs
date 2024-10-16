using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Application.Abstraction.Models.Basket
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Range(.1, double.MaxValue, ErrorMessage = "Invalid Product Price")]
        [Required]
        public decimal Price { get; set; }

        public string? PictureUrl { get; set; }

        [Range(1,int.MaxValue, ErrorMessage = "Invalid Product Quantity!")]
        [Required]
        public int Quantity { get; set; }

        public string? Brand { get; set; }

        public string? Category { get; set; }
    }
}
