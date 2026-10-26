CREATE TABLE [dbo].[KSBProgressStatusHistory]
(
    [KSBProgressStatusHistoryId] INT IDENTITY(1,1) NOT NULL,
    [KSBProgressId] INT NOT NULL,
    [Status] INT NULL,
    [StatusTime] DATETIME2(7) NULL,

    CONSTRAINT [PK_KSBProgressStatusHistory]
        PRIMARY KEY CLUSTERED ([KSBProgressStatusHistoryId] ASC)
) ON [PRIMARY];
GO

CREATE INDEX [IX_KSBProgressStatusHistory_KSBProgressId]
    ON [dbo].[KSBProgressStatusHistory] ([KSBProgressId]);
GO