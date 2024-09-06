using System;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    public class ApprenticeTaskDataFileRequest
    {
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileContents { get; set; }
    }
}