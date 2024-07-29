CREATE TABLE [dbo].[KSBProgressStatusHistory] (
    KSBProgressId BIGINT NOT NULL PRIMARY KEY,
    Status INT NULL, 
    StatusTime DATETIME2 NULL,
);
GO