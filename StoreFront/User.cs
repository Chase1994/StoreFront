namespace StoreFront
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please provide a username", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please provide a password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength =8, ErrorMessage ="Password must be 8 char long.")]
        public string[] Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirmation password does not match.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string ConfirmPassword { get; set; } 


        [Required(ErrorMessage = "Please provide an email address", AllowEmptyStrings = false)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage ="Please provide a valid email address")]
        public string EmailAddress { get; set; }

    }
}
