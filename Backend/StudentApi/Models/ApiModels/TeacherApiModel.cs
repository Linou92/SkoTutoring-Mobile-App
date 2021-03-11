using AppDbContext.Entities;
using AppDbContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentApi.Models.ApiModels
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
        public string FullName
        {
            get { return this.FirsName + " " + this.LastName; }
        }

        [DataMember]
        public string Role { get; set; } = Discriminator.Teacher.ToString();

        [DataMember]
        public List<LevelApiModel> Levels { get; set; }

        [DataMember]
        public List<LanguageApiModel> Languages { get; set; }

        [DataMember]
        public List<AvailabilityApiModel> Availabilities { get; set; }

        [DataMember]
        public List<SubjectApiModel> Subjects { get; set; }
    }

    [Serializable]
    [DataContract]
    public class AvailabilityApiModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StartTime { get; set; }

        [DataMember]
        public int EndTime { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public bool IsClosed { get; set; }
    }
}