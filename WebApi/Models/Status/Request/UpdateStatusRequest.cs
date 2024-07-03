using Domain;

namespace WebApi.Models.Status.Request;

public class UpdateStatusRequest
{
    public Guid Id { get; set; }
    
    public int CompletionPercent { get; set; }
    
    public StatusEnum StatusType { get; set; }
}