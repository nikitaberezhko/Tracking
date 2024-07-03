namespace WebApi.Models.Status.Response;

public class UpdateStatusResponse
{
    public Guid Id { get; set; }

    public int CompletionPercent { get; set; }
    
    public int StatusType { get; set; }
}