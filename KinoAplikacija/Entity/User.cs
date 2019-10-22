using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace KinoAplikacija.Entity
{
    [Serializable]
    [Class(Schema = "kinoaplikacija", Table = "Users", NameType = typeof(User))]
    public class User
    {
        [Id(Name = "Id", Column = "ID", Type = "int"), Generator(1, Class = "native")]
        public virtual int Id { get; set; }
        [Property(Column = "Name", Type = "string", NotNull = true, Length = 512)]
        public virtual string Name { get; set; }
        [Property(Column = "Surname", Type = "string", NotNull = true, Length = 512)]
        public virtual string Surname { get; set; }
        [Property(Column = "Email", Type = "string", NotNull = true, Length = 512)]
        public virtual string Email { get; set; }
        [Property(Column = "TelephoneNumber", Type = "string", NotNull = true, Length = 512)]
        public virtual string TelephoneNumber { get; set; }
        [Property(Column = "RightsLevel", Type = "string", NotNull = true, Length = 512)]
        public virtual string RightsLevel{ get; set; }//1-admin,..., 5-normal-user
        [Property(Column = "PasswordHash", Type = "string", NotNull = true, Length = 512)]
        public virtual string PasswordHash { get; set; }
        [Property(Column = "JoinDate", Type = "DateTime", NotNull = true)]
        public virtual DateTime JoinDate { get; set; }
        [Property(Column = "Birthday", Type = "DateTime")]
        public virtual DateTime? Birthday { get; set; }
        [Property(Column = "Address", Type = "string", Length = 512)]
        public virtual string Address { get; set; }
        [ManyToOne(ClassType = typeof(Place), Column = "FK_Place_Id")]
        public virtual Place Place { get; set; }

        [Bag(Table = "PasswordReset", Cascade = "all-delete-orphan", Inverse = true)]
        [Key(1, Column = "FK_User_Id")]
        [OneToMany(2, ClassType = typeof(PasswordReset))]
        public virtual IList<PasswordReset> PasswordResets { get; set; }

        [Bag(Table = "Reservations", Cascade = "all-delete-orphan", Inverse = true)]
        [Key(1, Column = "FK_User_Id")]
        [OneToMany(2, ClassType = typeof(Reservation))]
        public virtual IList<Reservation> Reservations { get; set; }
    }
}
