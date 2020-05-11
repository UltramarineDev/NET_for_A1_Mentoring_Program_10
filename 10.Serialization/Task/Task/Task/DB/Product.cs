namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Serializable]
    public partial class Product : ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        public Product(SerializationInfo info, StreamingContext context)
        {
            this.ProductID = (int)info.GetValue("ProductID", typeof(int));
            this.ProductName = (string)info.GetValue("ProductName", typeof(string));
            this.SupplierID = (int?)info.GetValue("SupplierID", typeof(int?));
            this.CategoryID = (int?)info.GetValue("CategoryID", typeof(int?));
            this.QuantityPerUnit = (string)info.GetValue("QuantityPerUnit", typeof(string));
            this.UnitPrice = (decimal)info.GetValue("UnitPrice", typeof(decimal));
            this.UnitsInStock = (short?)info.GetValue("UnitsInStock", typeof(short?));
            this.UnitsOnOrder = (short?)info.GetValue("UnitsOnOrder", typeof(short?));
            this.ReorderLevel = (short?)info.GetValue("ReorderLevel", typeof(short?));
            this.Discontinued = (bool)info.GetValue("Discontinued", typeof(bool));
            this.Category = (Category)info.GetValue("Category", typeof(Category));
            this.Supplier = (Supplier)info.GetValue("Supplier", typeof(Supplier));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ProductID", this.ProductID);
            info.AddValue("ProductName", this.ProductName);
            info.AddValue("SupplierID", this.SupplierID);
            info.AddValue("CategoryID", this.CategoryID);
            info.AddValue("QuantityPerUnit", this.QuantityPerUnit);
            info.AddValue("UnitPrice", this.UnitPrice);
            info.AddValue("UnitsInStock", this.UnitsInStock);
            info.AddValue("UnitsOnOrder", this.UnitsOnOrder);
            info.AddValue("ReorderLevel", this.ReorderLevel);
            info.AddValue("Discontinued", this.Discontinued);
            info.AddValue("Category", this.Category);
            info.AddValue("Order_Details", this.Order_Details);
            info.AddValue("Supplier", this.Supplier);

            IObjectContextAdapter objectContext = (context.Context as IObjectContextAdapter).ObjectContext;
            objectContext.ObjectContext.LoadProperty(this, p => p.Supplier);
            objectContext.ObjectContext.LoadProperty(this, p => p.Category);
            objectContext.ObjectContext.LoadProperty(this, p => p.Order_Details);
        }


        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
