using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Discounts", NameType = typeof(Discount))]
    public class Discount
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "Name", Type = "string", NotNull = true, Length = 512)]
        public virtual string Name{ get; set; }
        [Property(Column = "Percent", Type = "decimal", NotNull = true)]
        public virtual decimal Percent{ get; set; }
        [Property(Column = "FromDate", Type = "DateTime")]
        public virtual DateTime? FromDate{ get; set; }
        [Property(Column = "ToDate", Type = "DateTime")]
        public virtual DateTime? ToDate { get; set; }

        [Bag(Table = "Bills", Cascade = "all-delete-orphan", Inverse = true)]
        [Key(1, Column = "FK_Discount_Id")]
        [OneToMany(2, ClassType = typeof(Bill))]
        public virtual IList<Bill> Bills { get; set; }
    }
}
