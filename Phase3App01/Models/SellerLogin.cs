using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phase3App01.Models
{
    public class SellerLogin
    {
        [Key]
        public string Susername { get; set; }
        public string Spassword { get; set; }
    }
}
