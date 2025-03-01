namespace EasyVPN.Domain.Entities;

public class DynamicPage
{
    public string Route { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime LastModified  { get; set; }
    public DateTime Created  { get; set; }
    public string? Content { get; set; }
}