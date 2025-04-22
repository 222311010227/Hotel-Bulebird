using System.ComponentModel.DataAnnotations;

public class UserProfile
{
    [Key]
    public int UserId { get; set; }

    public int AppUserId { get; set; } // 👈 Add this line

    [Required]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    public string Mobile { get; set; }

    public string Address { get; set; }

    public string ProfilePicture { get; set; }  // path or URL
}
