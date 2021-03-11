using System;
using System.Runtime.Serialization;

namespace TeacherApi.Models.ApiModels
{
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

    [Serializable]
    [DataContract]
    public class CreateAvailabilityApiModel
    {
        [DataMember]
        public int StartTime { get; set; }

        [DataMember]
        public int EndTime { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public bool IsClosed { get; set; } = false;
    }
}