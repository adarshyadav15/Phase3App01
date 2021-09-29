using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Phase3App01.Models
{
   
    public class FinalModel
    {
        
        //public static int id = 1;
       public List<ShippingDetail> ShippingDetails { get; set; }
        public List<CartDetail11> CartDetail11 { get; set; }
    }
}
