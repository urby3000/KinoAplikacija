using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Theaters", NameType = typeof(Theater))]
    public class Theater
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "Name", Type = "string", NotNull = true, Length = 512)]
        public virtual string Name { get; set; }
        [Property(Column = "Address", Type = "string", NotNull = true, Length = 512)]
        public virtual string Address { get; set; }
        [ManyToOne(ClassType = typeof(Place), Column = "FK_Place_Id", NotNull = true)]
        public virtual Place Place { get; set; }

        [Bag(Table = "Rooms", Cascade = "all-delete-orphan", Inverse = true)]
        [Key(1, Column = "FK_Theater_Id")]
        [OneToMany(2, ClassType = typeof(Room))]
        public virtual IList<Room> Rooms { get; set; }
    }
}
