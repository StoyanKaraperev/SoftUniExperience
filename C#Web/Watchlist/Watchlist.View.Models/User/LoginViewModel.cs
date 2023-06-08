namespace Watchlist.View.Models.User;

using System.ComponentModel.DataAnnotations;
using static Watchlist.Common.ValidationConstants.Constants;

public class LoginViewModel
{
    [Required]
    [MinLength(UserEmailMinLength)]
    [MaxLength(UserUserNameMaxLength)]
    public string UserName { get; set; } = null!;

    [Required]
    [MinLength(UserEmailMinLength)]
    [MaxLength(UserEmailMaxLength)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
