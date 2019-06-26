using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRP.Models
{
    public class RushOrder
    {
        public int RushOrderId { get; set; }

        [StringLength(250), Required]
        [Display(Name = "Shipping Type")]
        public string ShippingType { get; set; }

        [Required]
        public decimal PriceLessThan1000 { get; set; }

        [Required]
        public decimal Price1000To2000 { get; set; }

        public decimal PriceGreater2000 { get; set; }
    }
}
