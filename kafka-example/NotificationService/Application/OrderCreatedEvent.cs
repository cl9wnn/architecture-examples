namespace Application;

public class OrderCreatedEvent
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}