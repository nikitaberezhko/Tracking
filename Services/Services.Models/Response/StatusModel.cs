namespace Services.Services.Models.Response;

public class StatusModel
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }
    
    public int CompletionPercent { get; set; }
    
    public int StatusType { get; set; }
}