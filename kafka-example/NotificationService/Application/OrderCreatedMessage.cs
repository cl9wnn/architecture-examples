namespace Application;

public class OrderCreatedMessage
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}