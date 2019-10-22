using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Rooms", NameType = typeof(Room))]
    public class Room
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "Name", Type = "string", NotNull = true, Length = 512)]
        public virtual string Name { get; set; }
        [Property(Column = "NumberOfSeats", Type = "int", NotNull = true)]
        public virtual int NumberOfSeats{ get; set; }
        [ManyToOne(ClassType = typeof(Theater), Column = "FK_Theater_Id", NotNull = true)]
        public virtual Theater Theater{ get; set; }

        [Bag(Table = "Events", Cascade = "all-delete-orphan", Inverse = true)]
        [Key(1, Column = "FK_Room_Id")]
        [OneToMany(2, ClassType = typeof(Event))]
        public virtual IList<Event> Events { get; set; }
    }
}
