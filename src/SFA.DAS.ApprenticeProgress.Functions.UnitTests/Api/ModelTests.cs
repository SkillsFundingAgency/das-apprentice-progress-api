using System;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Functions.Api;

namespace SFA.DAS.ApprenticeProgress.Functions.UnitTests
{
    public class ModelTests
    {
        [Test]
        public void ApprenticeTask_test()
        {
            var sut = new ApprenticeTask
            {
                TaskId = 1,
                ApprenticeshipId = 1,
                DueDate = new DateTime(2019, 05, 09),
                Title = "title",
                ApprenticeshipCategoryId = 0,
                Note = "note",
                CompletionDateTime = new DateTime(2019, 05, 09),
                CreatedDateTime = new DateTime(2019, 05, 09),
                Status = ApprenticeTask.TaskStatus.Done
            };

            Assert.AreEqual(1, sut.TaskId);
            Assert.AreEqual(1, sut.ApprenticeshipId);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.DueDate);
            Assert.AreEqual("title", sut.Title);
            Assert.AreEqual(0, sut.ApprenticeshipCategoryId);
            Assert.AreEqual("note", sut.Note);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.CompletionDateTime);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.CreatedDateTime);
            Assert.AreEqual(ApprenticeTask.TaskStatus.Done, sut.Status);
        }

        [Test]
        public void TaskReminder_test()
        {
            var sut = new TaskReminder
            {
                TaskId = 1,
                ReminderValue = 1,
                ReminderUnit = ReminderUnit.Days,
                ReminderStatus = ReminderStatus.Sent
            };

            Assert.AreEqual(1, sut.TaskId);
            Assert.AreEqual(1, sut.ReminderValue);
            Assert.AreEqual(ReminderUnit.Days, sut.ReminderUnit);
            Assert.AreEqual(ReminderStatus.Sent, sut.ReminderStatus);
        }
    }
}