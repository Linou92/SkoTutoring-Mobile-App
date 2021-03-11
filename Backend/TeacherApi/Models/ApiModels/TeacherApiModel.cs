using AppDbContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeacherApi.Models.ApiModels
{
    [Serializable]
    [DataContract]
    public class TeacherApiModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string FirsName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public Discriminator Role { get; set; } = Discriminator.Teacher;
    }
}