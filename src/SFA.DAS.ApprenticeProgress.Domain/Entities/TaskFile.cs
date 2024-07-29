using System;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    public class TaskFile
    {
        public int TaskId  { get; set; }
        public int? TaskFileId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public byte[] FileContents { get; set; }
    }
}