CREATE TABLE [dbo].[ProgressNotification]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [NotificationId] NVARCHAR(50) NOT NULL,
    [NotificationScope] NVARCHAR(50) NOT NULL,
    [IsEnabled] BIT NOT NULL,
    [ActivationPoint] INT NOT NULL,
    [Delay] NVARCHAR(50) NOT NULL,
    [DelayUnit] INT NOT NULL
);
GO