using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreServices.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
    }
}
