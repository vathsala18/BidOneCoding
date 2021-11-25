using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BidOneCoding.Models
{
    public class Person
    {
        [RegularExpression(@"[a-zA-Z]*$", ErrorMessage ="First name can only have alphabets")]
        [StringLength(20, MinimumLength =3, ErrorMessage = "First name should be between 3 and 20 characters")]
        [Required(ErrorMessage = "Please enter your first name")]
        [Remote(action: "ValidateFirstName", controller: "Validation")]
        public string FirstName { get; set; }

        [RegularExpression(@"[a-zA-Z]*$", ErrorMessage = "Last name can only have alphabets")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last name should be between 3 and 20 characters")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }
    }
}
