using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "MovieGenre", NameType = typeof(MovieGenre))]
    public class MovieGenre
    {

        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [ManyToOne(ClassType = typeof(Movie), Column = "FK_Movie_Id", NotNull = true)]
        public virtual Movie Movie{ get; set; }
        [ManyToOne(ClassType = typeof(Genre), Column = "FK_Genre_Id", NotNull = true)]
        public virtual Genre Genre { get; set; }

    }
}
