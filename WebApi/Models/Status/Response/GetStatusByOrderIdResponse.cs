namespace WebApi.Models.Status.Response;

public class GetStatusByOrderIdResponse
{
    public Guid Id { get; set; }
    
    public int CompletionPercent { get; set; }
    
    public int StatusType { get; set; }
}