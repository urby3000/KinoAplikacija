using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Bills", NameType = typeof(Bill))]
    public class Bill
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "OrderDate", Type = "DateTime", NotNull = true)]
        public virtual DateTime OrderDate { get; set; }
        [Property(Column = "PayDate", Type = "DateTime")]
        public virtual DateTime? PayDate { get; set; }
        [Property(Column = "Paid", Type = "bool")]
        public virtual bool Paid { get; set; }
        [Property(Column = "Price", Type = "decimal", NotNull = false)]
        public virtual decimal Price { get; set; }
        [Property(Column = "FullPrice", Type = "decimal", NotNull = false)]
        public virtual decimal FullPrice{ get; set; }
        [ManyToOne(ClassType = typeof(Discount), Column = "FK_Discount_Id")]
        public virtual Discount Discount { get; set; }

        [Bag(Table = "Reservations", Cascade = "all-delete-orphan", Inverse = true)]
        [Key(1, Column = "FK_Bill_Id")]
        [OneToMany(2, ClassType = typeof(Reservation))]
        public virtual IList<Reservation> Reservations { get; set; }
    }
}
