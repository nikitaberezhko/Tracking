using Domain;

namespace Services.Services.Models.Request;

public class UpdateStatusModel
{
    public Guid Id { get; set; }
    
    public int CompletionPercent { get; set; }
    
    public StatusEnum StatusType { get; set; }
}