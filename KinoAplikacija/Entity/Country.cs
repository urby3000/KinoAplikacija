using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table ="Countries",NameType =typeof(Country))]
    public class Country
    {
        [Id(Name= "Id", Column ="ID",Type ="int"),Generator(1,Class= "native")]
        public virtual int Id { get; set; }
        [Property(Column ="Name",Type ="string",NotNull =true,Length =256)]
        public virtual string Name { get; set; }
        [Property(Column = "Abbreviation", Type = "string", NotNull = true, Length = 128)]
        public virtual string Abbreviation { get; set; }

        [Bag(Table ="Places",Cascade = "all-delete-orphan", Inverse =true)]
        [Key(1,Column ="FK_Country_Id")]
        [OneToMany(2,ClassType =typeof(Place))]
        public virtual IList<Place> Places { get; set; }
    }
    
}
