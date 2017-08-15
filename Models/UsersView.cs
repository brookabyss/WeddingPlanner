using System.ComponentModel.DataAnnotations;

namespace wedding.Models
{
    public class UsersView
    {
        [Required]
        [MinLength(2, ErrorMessage="First name has to be 2 characters in length at least")]
        public string FirstName {get;set;}
        [Required]
        [MinLength(2, ErrorMessage="Last name has to be 2 characters in length at least")]
        public string LastName {get;set;}
        // [Required]
        // [Range(0,int.MaxValue)]
        // public int Age {get;set;}
        [Required]
        [EmailAddress (ErrorMessage="Incorrect email format")]
        public string Email {get;set;}
        [Required]
        [MinLength(8, ErrorMessage="Password needs to be at least 8 characters in length")]
        public string Password {get;set;}
        [Required]
        [MinLength(8)]
        [Compare("Password",ErrorMessage="Confirm password doesn't match password")]
        public string ConfirmPassword {get;set;}
    }

}