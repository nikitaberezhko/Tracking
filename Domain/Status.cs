namespace Domain;

public class Status
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }
    
    public int CompletionPercent { get; set; }
    
    public StatusEnum StatusType { get; set; }
}