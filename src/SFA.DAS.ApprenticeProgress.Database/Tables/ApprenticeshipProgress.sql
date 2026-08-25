CREATE TABLE [dbo].[ApprenticeshipProgress]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [ApprenticeAccountId] UNIQUEIDENTIFIER NOT NULL,
    [ApprenticeshipId] NCHAR(10) NULL,
    [FirstLoggedIn] DATETIME2 NOT NULL,
    [LastLoggedIn] DATETIME2 NULL,
    [StartDate] DATETIME2 NULL,
    [PlannedEndDate] DATETIME2 NULL,
    [IsEnabled] BIT NOT NULL
);
GO