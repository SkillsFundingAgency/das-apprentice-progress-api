CREATE TABLE [dbo].[ApprenticeshipProgressNotification]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [ApprenitceProgressId] INT NOT NULL,
    [NotificationId] UNIQUEIDENTIFIER NOT NULL,
    [IsEnabled] BIT NOT NULL,

    CONSTRAINT [FK_ApprenticeshipProgressNotification_ApprenticeshipProgress]
        FOREIGN KEY ([ApprenitceProgressId])
        REFERENCES [dbo].[ApprenticeshipProgress]([Id]),

    CONSTRAINT [FK_ApprenticeshipProgressNotification_ProgressNotification]
        FOREIGN KEY ([NotificationId])
        REFERENCES [dbo].[ProgressNotification]([Id])
);
GO