using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace StoreFront.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class Users
    {
        public string ConfirmPassword { get; set; }
    }

    public class UserMetadata
    {
        [Display(Name= "Username")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="UserName is required.")]
        public string UserName { get; set; }

        [Display(Name="Email Address")]
        [Required(AllowEmptyStrings =false, ErrorMessage="Email Address is required.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage ="A minimum of 6 characters is required.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [EqualTo("Password", ErrorMessage="Confirm Password and password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}