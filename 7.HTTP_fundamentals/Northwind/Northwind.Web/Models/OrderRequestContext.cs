using System;

namespace Northwind.Web
{
    public class OrderRequestContext
    {
        public string CustomerId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
}