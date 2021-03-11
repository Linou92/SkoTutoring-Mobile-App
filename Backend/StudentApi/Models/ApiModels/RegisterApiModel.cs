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
    public class RegisterTeacherApiModel
    {
        [DataMember]
        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(50)]
        public string UserName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(255,ErrorMessage = "Must be between 6 and 255 Length",MinimumLength = 6)]
        public string Password { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 6 and 255 Length", MinimumLength = 6)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DataMember]
        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        public HttpPostedFile ImageUrl { get; set; }
    }
}