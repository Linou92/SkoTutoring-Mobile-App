using AppDbContext.Enums;
using System;
using System.Runtime.Serialization;

namespace TeacherApi.Models.ApiModels
{
    [Serializable]
    [DataContract]
    public class StudentApiModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Discriminator Role { get; set; } = Discriminator.Student;
    }
}