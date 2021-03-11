using AppDbContext.Enums;
using System;
using System.Runtime.Serialization;

namespace StudentApi.Models.ApiModels
{
    [Serializable]
    [DataContract]
    public class StudentApiModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Role { get; set; } = Discriminator.Student.ToString();
    }
}