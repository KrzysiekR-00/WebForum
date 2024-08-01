using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebForum.Models;

public class ApplicationUser : IdentityUser
{
    [StringLength(16, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
    public string Login { get; set; } = null!;
}
