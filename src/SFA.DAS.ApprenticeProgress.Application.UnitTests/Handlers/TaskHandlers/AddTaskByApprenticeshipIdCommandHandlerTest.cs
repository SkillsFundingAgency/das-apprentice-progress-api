﻿using System;
using System.Linq;
using System.Threading;
using AutoFixture;
using NUnit.Framework;
using SFA.DAS.ApprenticeProgress.Application.Commands;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ApprenticeProgress.Application.UnitTests.DataFixture
{
    public class AddTaskByApprenticeshipIdCommandHandlerTest : ApprenticeProgressDbContextFixture
    {
        private readonly Fixture _fixture = new();

        [Test, MoqAutoData]
        public async System.Threading.Tasks.Task AddOrUpdateKsbProgressCommandHandler_Test()
        {
            await PopulateDbContext();

            var addTaskByApprenticeshipIdCommandHandler = new AddTaskByApprenticeshipIdCommandHandler(DbContext);

            var command = new AddTaskByApprenticeshipIdCommand()
            {
                TaskId = 1,
                ApprenticeshipId = 1,
                ApprenticeAccountId = new Guid(),
                DueDate = new DateTime(2019, 05, 09),
                Title = "title",
                ApprenticeshipCategoryId = 0,
                Note = "note",
                CompletionDateTime = new DateTime(2019, 05, 09),
                CreatedDateTime = new DateTime(2019, 05, 09),
                Status = 1,
                ReminderStatus = 1,
                ReminderUnit = 1,
                ReminderValue = 1
            };
          
            var result = await addTaskByApprenticeshipIdCommandHandler.Handle(command, CancellationToken.None);
            Assert.That(result, Is.Not.Null);
        }

        private async System.Threading.Tasks.Task PopulateDbContext()
        {
            var ksbprogress = _fixture.CreateMany<Domain.Entities.Task>().ToArray();
            await DbContext.Task.AddRangeAsync(ksbprogress);
            await DbContext.SaveChangesAsync();
        }
    }
}