using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentApi.Models.ApiModels
{
    [Serializable]
    [DataContract]
    public class CreateSubjectApiModel
    {
       // public int Id { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }
    }

    [Serializable]
    [DataContract]
    public class SubjectApiModel
    {
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<TeacherApiModel> Teachers { get; set; }
    }
}