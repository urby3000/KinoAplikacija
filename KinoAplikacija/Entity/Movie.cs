using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Movies", NameType = typeof(Movie))]
    public class Movie
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "Title", Type = "string", NotNull = true, Length = 512)]
        public virtual string Title { get; set; }
        [Property(Column = "Description", Type = "string", NotNull = true, Length = 1028)]
        public virtual string Description { get; set; }
        [Property(Column = "ImageSource", Type = "string", NotNull = true, Length = 1028)]
        public virtual string ImageSource { get; set; }

        [Bag(Table = "MovieGenre", Cascade = "all-delete-orphan",Inverse =true)]
        [Key(1, Column = "FK_Movie_Id")]
        [OneToMany(2, ClassType = typeof(MovieGenre))]
        public virtual IList<MovieGenre> MoviesGenres{ get; set; }

        [Bag(Table = "Events", Cascade = "all-delete-orphan", Inverse = false)]
        [Key(1, Column = "FK_Movie_Id")]
        [OneToMany(2, ClassType = typeof(Event))]
        public virtual IList<Event> Events { get; set; }

    }
}
