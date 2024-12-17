using System.Threading.Tasks;
using RestEase;
using SFA.DAS.ApprenticeProgress.Functions.Api.Response;

namespace SFA.DAS.ApprenticeProgress.Functions.Api.Clients
{
    public interface IApprenticeProgressApiClient
    {
        [Get("/apprenticeships/gettaskreminders/")]
        Task<TaskRemindersWrapper> GetTaskReminders();

        [Post("/apprenticeships/updatetaskreminders/tasks/{taskId}/status/{statusId}")]
        Task UpdateTaskReminders([Path] int taskId, [Path] int statusId);
    }
}
