using System;

namespace Northwind.Data.Entities
{
    public class CreditDetail
    {
        public int CreditDetailId { get; set; }
        public string CustomerId { get; set; }
        public int CardNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string CardHolderName { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
