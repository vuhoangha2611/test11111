//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FPT_Final.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Staff
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        [Required(ErrorMessage ="You must input Email")]

        public string Email { get; set; }
        [Required(ErrorMessage = "You must input Password")]
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
