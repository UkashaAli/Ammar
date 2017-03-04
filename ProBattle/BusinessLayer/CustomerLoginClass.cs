using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class CustomerLoginClass
    {
        [Required]
        public string customerEmail { get; set; }
        [Required]
        public string customerPassword { get; set; }
    }
}
