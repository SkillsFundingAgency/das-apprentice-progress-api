CREATE TABLE [dbo].[ApprenticeshipProgressNotification]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ApprenticeProgressId] INT NOT NULL,
    [NotificationId] UNIQUEIDENTIFIER NOT NULL,
    [IsEnabled] BIT NOT NULL,

    CONSTRAINT [FK_ApprenticeshipProgressNotification_ApprenticeshipProgress]
        FOREIGN KEY ([ApprenticeProgressId])
        REFERENCES [dbo].[ApprenticeshipProgress]([Id]),

    CONSTRAINT [FK_ApprenticeshipProgressNotification_ProgressNotification]
        FOREIGN KEY ([NotificationId])
        REFERENCES [dbo].[ProgressNotification]([Id])
);
GO