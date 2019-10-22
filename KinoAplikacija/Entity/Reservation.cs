using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Reservations", NameType = typeof(Reservation))]
    public class Reservation
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "SeatNumber", Type = "int", NotNull = true)]
        public virtual int SeatNumber { get; set; }
        [ManyToOne(ClassType = typeof(User), Column = "FK_User_Id", NotNull = true)]
        public virtual User User{ get; set; }
        [ManyToOne(ClassType = typeof(Event), Column = "FK_Event_Id", NotNull = true)]
        public virtual Event Event{ get; set; }
        [ManyToOne(ClassType = typeof(Bill), Column = "FK_Bill_Id")]
        public virtual Bill Bill{ get; set; }
    }
}
