namespace MyRestaurantApi; 
public class MenuItem {
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public MenuItemCategory? Category { get; set; }
}
