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
    public class RequestSessionModel
    {

        [DataMember]
        [Required]
        public string Title { get; set; }

        [DataMember]
        [Required]
        public int AvailabilityId { get; set; }

        [DataMember]
        public string Subject { get; set; }
        
        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public string Language { get; set; }

        //[DataMember]
        //[Required]
        //public string TutorId { get; set; }

        //[DataMember]
        //public string Purpose { get; set; }

    }
}