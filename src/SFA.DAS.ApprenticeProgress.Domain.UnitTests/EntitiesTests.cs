using System;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Domain.Entities;

namespace SFA.DAS.ApprenticeProgress.Domain.UnitTests
{
    public class EntitiesTests
    {
        [Test]
        public void ApprenticeshipCategory_Entity_Test()
        {
            var sut = new ApprenticeshipCategory
            {
                CategoryId = 1,
                Title = "title"
            };

            Assert.AreEqual("title", sut.Title);
            Assert.AreEqual(1, sut.CategoryId);
        }

        [Test]
        public void KSBProgress_Entity_Test()
        {
            var sut = new KSBProgress
            {
                ApprenticeshipId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                KSBProgressId = 1,
                KSBProgressType = KSBProgressType.Behaviour,
                KSBId = new Guid("0D2B0228-4D0D-4C23-8B49-01A698857709"),
                KSBKey = "key",
                CurrentStatus = KSBStatus.Completed,
                Note = "note"
            };

            Assert.AreEqual(new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"), sut.ApprenticeshipId);
            Assert.AreEqual(1, sut.KSBProgressId);
            Assert.AreEqual(KSBProgressType.Behaviour, sut.KSBProgressType);
            Assert.AreEqual(new Guid("0D2B0228-4D0D-4C23-8B49-01A698857709"), sut.KSBId);
            Assert.AreEqual("key", sut.KSBKey);
            Assert.AreEqual(KSBStatus.Completed, sut.CurrentStatus);
            Assert.AreEqual("note", sut.Note);
        }

        [Test]
        public void KSBProgressStatusHistory_Entity_Test()
        {
            var sut = new KSBProgressStatusHistory
            {
                KSBProgressId = 1,
                Status = 0,
                StatusTime = new DateTime(2019, 05, 09)
            };


            Assert.AreEqual(1, sut.KSBProgressId);
            Assert.AreEqual(0, sut.Status);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.StatusTime);
        }

        [Test]
        public void Task_Entity_test()
        {
            var sut = new Task
            {
                TaskId = 1,
                ApprenticeshipId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                DueDate = new DateTime(2019, 05, 09),
                Title = "title",
                ApprenticeshipCategoryId = 0,
                Note = "note",
                CompletionDateTime = new DateTime(2019, 05, 09),
                CreatedDateTime = new DateTime(2019, 05, 09),
                Status = Task.TaskStatus.Done
            };

            Assert.AreEqual(1, sut.TaskId);
            Assert.AreEqual(new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"), sut.ApprenticeshipId);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.DueDate);
            Assert.AreEqual("title", sut.Title);
            Assert.AreEqual(0, sut.ApprenticeshipCategoryId);
            Assert.AreEqual("note", sut.Note);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.CompletionDateTime);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.CreatedDateTime);
            Assert.AreEqual(Task.TaskStatus.Done, sut.Status);
        }

        [Test]
        public void TaskCategory_Entity_Test()
        {
            var sut = new TaskCategory
            {
                TaskId = 1,
                CategoryId = 2
            };

            Assert.AreEqual(1, sut.TaskId);
            Assert.AreEqual(2, sut.CategoryId);
        }

        [Test]
        public void TaskFile_Entity_Test()
        {
            var sut = new TaskFile
            {
                TaskId = 1,
                TaskFileId = 2,
                FileType = "type",
                FileName = "name",
                FileContents = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            Assert.AreEqual(1, sut.TaskId);
            Assert.AreEqual(2, sut.TaskFileId);
            Assert.AreEqual("type", sut.FileType);
            Assert.AreEqual("name", sut.FileName);
            Assert.AreEqual(new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, sut.FileContents);
        }


        [Test]
        public void TaskKSBs_Entity_Test()
        {
            var sut = new TaskKSBs
            {
                TaskId = 1,
                KSBProgressId = 2
            };

            Assert.AreEqual(1, sut.TaskId);
            Assert.AreEqual(2, sut.KSBProgressId);
        }

        [Test]
        public void TaskReminder_Entity_Test()
        {
            var sut = new TaskReminder
            {
                TaskId = 1,
                ReminderId = 2,
                ReminderValue = 3,
                ReminderUnit = ReminderUnit.Days,
                Status = ReminderStatus.Dismissed
            };

            Assert.AreEqual(1, sut.TaskId);
            Assert.AreEqual(2, sut.ReminderId);
            Assert.AreEqual(3, sut.ReminderValue);
            Assert.AreEqual(ReminderUnit.Days, sut.ReminderUnit);
            Assert.AreEqual(ReminderStatus.Dismissed, sut.Status);
        }
    }
}