namespace Watchlist.View.Models.User;

using System.ComponentModel.DataAnnotations;
using static Watchlist.Common.ValidationConstants.Constants;

public class RegisterViewModel
{
    [Required]
    [MinLength(UserUserNameMinLength)]
    [MaxLength(UserUserNameMaxLength)]
    public string UserName { get; set; } = null!; 

    [Required]
    [EmailAddress]
    [MinLength(UserEmailMinLength)]
    [MaxLength(UserEmailMaxLength)]
    public string Email { get; set;} = null!;

    [Required]
    [DataType(DataType.Password)]
    [MinLength(UserPasswordMinLength)]
    [MaxLength(UserPasswordlMaxLength)]
    public string Password { get; set; } = null!;

    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!; 
}
