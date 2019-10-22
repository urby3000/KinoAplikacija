using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "PasswordReset", NameType = typeof(PasswordReset))]
    public class PasswordReset
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "SecurityCode", Type = "string", NotNull = true, Length = 512)]
        public virtual string SecurityCode{ get; set; }
        [Property(Column = "ResetDate", Type = "DateTime")]
        public virtual DateTime? ResetDate { get; set; }
        [Property(Column = "Reset", Type = "bool")]
        public virtual bool Reset{ get; set; }
        [ManyToOne(ClassType = typeof(User), Column = "FK_User_Id", NotNull = true)]
        public virtual User User{ get; set; }

    }
}
