using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CoreSerivce.BO
{
    [DataContract]
    public class Users
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }


        [DataMember]
        public string UserName { get; set; }


        [DataMember]
        public string Email { get; set; }


        [DataMember]
        public string Password { get; set; }


        [DataMember]
        public short ActiveState { get; set; }


        [DataMember]
        public string CellPhone { get; set; }


        public string Registered { get; set; }

        public string PasswordChanged { get; set; }

         [DataMember]
        public string LastLogin { get; set; }

        [DataMember]
        public double MenuSecurityKey { get; set; }

        [DataMember]
        public double AccessSecurityKey { get; set; }

        [DataMember]
        public List<Users_Group> Groups { get; set; }


        [DataMember]
        public string SessionKey { get; set; }


        public string SessionKeyExpire { get; set; }

        [DataMember]
        public string ProfilePicture { get; set; }

       
    }
}
