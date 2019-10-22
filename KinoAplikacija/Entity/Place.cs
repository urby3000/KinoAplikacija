using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Places", NameType = typeof(Place))]
    public class Place
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "Name", Type = "string", NotNull = true, Length = 256)]
        public virtual string Name { get; set; }
        [Property(Column = "PostalCode", Type = "string", NotNull = true, Length = 256)]
        public virtual string PostalCode { get; set; }
        [ManyToOne(ClassType =typeof(Country),Column ="FK_Country_Id",NotNull =true)]
        public virtual Country Country { get; set; }

        [Bag(Table = "Users", Cascade = "save-update", Inverse = false)]
        [Key(1, Column = "FK_Place_Id")]
        [OneToMany(2, ClassType = typeof(User))]
        public virtual IList<User> Users{ get; set; }

        [Bag(Table = "Theaters", Cascade = "all-delete-orphan", Inverse = true)]
        [Key(1, Column = "FK_Place_Id")]
        [OneToMany(2, ClassType = typeof(Theater))]
        public virtual IList<Theater> Theaters { get; set; }
    }
}
