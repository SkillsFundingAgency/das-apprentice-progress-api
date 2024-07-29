using System;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    public class TaskFile
    {
        public int TaskId  { get; set; }
        public int? TaskFileId { get; set; }
        public string FileType { get; set; }
    }
}