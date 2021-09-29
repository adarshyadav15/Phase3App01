using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Phase3App01.Models
{
    public partial class ShippingDetail
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public int? Phone { get; set; }
        [Key]
        public int Id { get; set; }
    }
}
