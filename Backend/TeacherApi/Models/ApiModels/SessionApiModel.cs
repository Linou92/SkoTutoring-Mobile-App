﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeacherApi.Models.ApiModels
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
        public int SubjectId { get; set; }

        [DataMember]
        public string Subject { get; set; }


        [DataMember]
        public int LevelId { get; set; }

        [DataMember]
        public string Level { get; set; }

        [DataMember]
        public string TeacherId { get; set; }

        [DataMember]
        public string Teacher { get; set; }

        [DataMember]
        public string Student { get; set; }

        [DataMember]
        public string StudentId { get; set; }

        [DataMember]
        public int LangId { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public int Purpose { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public int AvailabilityId { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string Notes { get; set; }
    }
}