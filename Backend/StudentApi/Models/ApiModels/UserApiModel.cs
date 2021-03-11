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
    public class UserApiModel
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Role { get; set; } = Discriminator.Student.ToString();

        [DataMember]
        public string Token { get; set; }
    }

    [Serializable]
    [DataContract]
    public class RequestTokenModel
    {

        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public string token_type { get; set; }

        [DataMember]
        public string expires_in { get; set; }

        [DataMember]
        public string userName { get; set; }
    }
}