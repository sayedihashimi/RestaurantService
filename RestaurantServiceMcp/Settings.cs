using System.ComponentModel.DataAnnotations;
public class Settings
{
    public const string SectionName = "Config";

    [Required]
    public string RestaurantApiBaseUrl { get; set; } = "http://localhost:5000"; // Default value
}