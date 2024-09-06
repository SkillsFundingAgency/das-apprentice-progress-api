using System;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Models;
using static SFA.DAS.ApprenticeProgress.Api.Controllers.TaskController;

namespace SFA.DAS.ApprenticeApp.UnitTests
{
    public class ModelTests
    {
        [Test]
        public void ApprenticeTaskDataRequest_test()
        {
            var sut = new ApprenticeTaskDataRequest
            {
                TaskId = 1,
                ApprenticeshipId = 1,
                DueDate = new DateTime(2019, 05, 09),
                Title = "title",
                ApprenticeshipCategoryId = 0,
                Note = "note",
                CompletionDateTime = new DateTime(2019, 05, 09),
                CreatedDateTime = new DateTime(2019, 05, 09),
                CategoryId = 1,
                Status = 1,
                ReminderValue = 1,
                ReminderUnit = 1,
                ReminderStatus = 1
            };

            Assert.AreEqual(1, sut.TaskId);
            Assert.AreEqual(1, sut.ApprenticeshipId);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.DueDate);
            Assert.AreEqual("title", sut.Title);
            Assert.AreEqual(0, sut.ApprenticeshipCategoryId);
            Assert.AreEqual("note", sut.Note);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.CompletionDateTime);
            Assert.AreEqual(new DateTime(2019, 05, 09), sut.CreatedDateTime);
            Assert.AreEqual(1, sut.Status);
            Assert.AreEqual(1, sut.CategoryId);
            Assert.AreEqual(1, sut.Status);
            Assert.AreEqual(1, sut.ReminderValue);
            Assert.AreEqual(1, sut.ReminderUnit);
            Assert.AreEqual(1, sut.ReminderStatus);
        }

        [Test]
        public void ApprenticeTaskDataFileRequest_test()
        {
            var sut = new ApprenticeTaskDataFileRequest
            {
                FileContents = "contents",
                FileName = "name",
                FileType = "type"
            };

            Assert.AreEqual("type", sut.FileType);
            Assert.AreEqual("name", sut.FileName);
            Assert.AreEqual("contents", sut.FileContents);
        }
    }
}