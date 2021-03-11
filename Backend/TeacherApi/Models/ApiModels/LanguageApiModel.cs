using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeacherApi.Models.ApiModels
{
    [Serializable]
    [DataContract]
    public class LanguageApiModel
    {
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }

        public List<TeacherApiModel> Teachers { get; set; }
    }

    [Serializable]
    [DataContract]
    public class CreateLanguageApiModel
    {
        // public int Id { get; set; }
        [Required]
        [DataMember]
        public string Name { get; set; }
    }
}