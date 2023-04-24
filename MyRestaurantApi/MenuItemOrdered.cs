namespace MyRestaurantApi; 
public class MenuItemOrdered {
    public MenuItemOrdered() { }
    public MenuItemOrdered BuildFromMenuItem(MenuItem item, int togoOrderId) => new MenuItemOrdered {
        TogoOrderId = togoOrderId,
        Name = item.Name,
        Price = item.Price,
        Category = item.Category
    };
    public int Id { get; set; }
    public int MenuItemId { get; set; }
    public int TogoOrderId { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public MenuItemCategory? Category { get; set; }

}
