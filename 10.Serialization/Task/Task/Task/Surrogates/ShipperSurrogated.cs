using System.Collections.Generic;
using System.Runtime.Serialization;
using Task.DB;

namespace Task
{
    [DataContract(Name = "Shipper")]
    public class ShipperSurrogated
    {
        [DataMember]
        public int ShipperID { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public List<Order> Orders { get; set; }
    }
}
