using System.Runtime.Serialization;
using Task.DB;

namespace Task
{
    [DataContract(Name = "Order_Detail")]
    public class OrderDetailSurrogated
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public short Quantity { get; set; }

        [DataMember]
        public float Discount { get; set; }

        [DataMember]
        public Order Order { get; set; }

        [DataMember]
        public Product Product { get; set; }
    }
}
