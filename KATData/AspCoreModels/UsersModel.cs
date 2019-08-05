using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AspCoreModels
{
    [DataContract]
    public class UsersModel
    {
        [DataMember(Name = "userid")]
        public string userid { get; set; }

        [DataMember(Name = "password")]
        public string password { get; set; }
 
    }
}
