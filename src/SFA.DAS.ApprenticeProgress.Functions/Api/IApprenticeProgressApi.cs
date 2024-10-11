using Microsoft.AspNetCore.Mvc;
using RestEase;

namespace SFA.DAS.ApprenticeProgress.Functions.Api
{
    public interface IApprenticeProgressApi
    { 
        [Get("/apprenticeships/gettaskreminders")]
        Task<TaskRemindersWrapper> GetTaskReminders();

        [Post("/apprenticeships/updatetaskreminders/")]
        Task UpdateTaskReminders(int taskId, int statusId);
    }
}