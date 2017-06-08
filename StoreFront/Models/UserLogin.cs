using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.Models
{
    public class UserLogin
    {
        [Required(AllowEmptyStrings = false, ErrorMessage ="Username is required.")]
        [Display(Name ="Username")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Password is required.")]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}