using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Customer
    {
        [Required]
        public string customerName { get; set; }
        [Required]
        public string customerEmail { get; set; }
        [Required]
        public string customerPassword { get; set; }
        [Required]
        public string customerType { get; set; }
        [Required]
        public string customerAddress { get; set; }

    }
}
