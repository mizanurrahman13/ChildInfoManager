using System.ComponentModel.DataAnnotations;

namespace CHILDINFOMANAGER.Web.Entities;

public class Children
{
    public Guid Id { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Date of Birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "Home Address")]
    public string HomeAddress { get; set; } = string.Empty;
}
