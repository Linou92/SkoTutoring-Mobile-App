using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentApi.Models.ApiModels
{
    [Serializable]
    [DataContract]
    public class SessionApiModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public SubjectApiModel Subject { get; set; }

        [DataMember]
        public string Level { get; set; }

        [DataMember]
        public TeacherApiModel Teacher { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public int Purpose { get; set; }

        [DataMember]
        public AvailabilityApiModel Availability { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string Notes { get; set; }
    }
}