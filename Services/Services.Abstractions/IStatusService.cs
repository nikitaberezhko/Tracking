using Services.Services.Models.Request;
using Services.Services.Models.Response;

namespace Services.Services.Abstractions;

public interface IStatusService
{
    public Task<StatusModel> GetStatus(GetStatusModel model);

    public Task<StatusModel> GetStatusByOrderId(GetStatusByOrderIdModel model);

    public Task<StatusModel> UpdateStatus(UpdateStatusModel model);
}