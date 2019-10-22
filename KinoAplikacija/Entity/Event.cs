using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Events", NameType = typeof(Event))]
    public class Event
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "Date", Type = "DateTime", NotNull = true)]
        public virtual DateTime Date { get; set; }
        [Property(Column = "Price", Type = "decimal", NotNull = true)]
        public virtual decimal Price { get; set; }
        [ManyToOne(ClassType = typeof(Movie), Column = "FK_Movie_Id", NotNull = true)]
        public virtual Movie Movie{ get; set; }
        [ManyToOne(ClassType = typeof(Room), Column = "FK_Room_Id", NotNull = true)]
        public virtual Room Room{ get; set; }

        [Bag(Table = "Reservations", Cascade = "all-delete-orphan", Inverse = true)]
        [Key(1, Column = "FK_Event_Id")]
        [OneToMany(2, ClassType = typeof(Reservation))]
        public virtual IList<Reservation> Reservations { get; set; }
    }
}
