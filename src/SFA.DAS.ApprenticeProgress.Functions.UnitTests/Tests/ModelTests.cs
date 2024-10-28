using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SFA.DAS.ApprenticeProgress.Functions.Api.Response;

namespace SFA.DAS.ApprenticeProgress.Functions.UnitTests
{
    public class ModelTests
    {
        [Test]
        public void ApprenticeTask_test()
        {
            var sut = new TaskReminderModel
            {
                TaskId = 1,
                ApprenticeshipId = 1,
                DueDate = new DateTime(2019, 05, 09),
                Title = "title",
                ApprenticeshipCategoryId = 0,
                Note = "note",
                CompletionDateTime = new DateTime(2019, 05, 09),
                CreatedDateTime = new DateTime(2019, 05, 09),
                ReminderStatus = ReminderStatus.NotSent,
                ReminderValue = 1,
                ReminderUnit = ReminderUnit.Days
            };

            ClassicAssert.IsTrue(1 == sut.TaskId);
            ClassicAssert.IsTrue(1 == sut.ApprenticeshipId);
            ClassicAssert.IsTrue(new DateTime(2019, 05, 09) == sut.DueDate);
            ClassicAssert.IsTrue("title" == sut.Title);
            ClassicAssert.IsTrue(0 == sut.ApprenticeshipCategoryId);
            ClassicAssert.IsTrue("note" == sut.Note);
            ClassicAssert.IsTrue(new DateTime(2019, 05, 09) == sut.CompletionDateTime);
            ClassicAssert.IsTrue(new DateTime(2019, 05, 09) == sut.CreatedDateTime);
            ClassicAssert.IsTrue(ReminderStatus.NotSent == sut.ReminderStatus);
            ClassicAssert.IsTrue(1 == sut.ReminderValue);
            ClassicAssert.IsTrue(ReminderUnit.Days == sut.ReminderUnit);
        }


    }
}