using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Genres", NameType = typeof(Genre))]
    public class Genre
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "Name", Type = "string", NotNull = true, Length = 512)]
        public virtual string Name { get; set; }

        [Bag(Table = "MovieGenre", Cascade = "all-delete-orphan", Inverse = true)]
        [Key(1, Column = "FK_Genre_Id")]
        [OneToMany(2, ClassType = typeof(MovieGenre))]
        [XmlIgnoreAttribute]
        public virtual IList<MovieGenre> MoviesGenres { get; set; }

    }
}
