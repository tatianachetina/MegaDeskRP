using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRP.Models
{
    public class DeskQuote
    {


        public int DeskQuoteId { get; set; }

        public int DeskId { get; set; }
        public Desk Desk { get; set; }

        [Display(Name = "Customer Name")]
        [Required]
        public string CustomerName { get; set; }

        
        [Display(Name = "Shipping Type")]
        public RushOrder RushOrder { get; set; }
        public int RushOrderId { get; set; }

        [Display(Name = "Quote Price")]
        public decimal QuotePrice { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime QuoteDate { get; set; }

       
    }
}
