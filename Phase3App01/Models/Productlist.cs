using System;
using System.Collections.Generic;

#nullable disable

namespace Phase3App01.Models
{
    public partial class Productlist
    {
        public int Pid { get; set; }
        public string Pname { get; set; }
        public int? Pprice { get; set; }
        public string Pdetails { get; set; }
        public string Sname { get; set; }
    }
}
