﻿CREATE TABLE [dbo].[KSBProgressStatusHistory](
    [KSBProgressId] [bigint] NOT NULL,
    [Status] [int] NULL,
    [StatusTime] [datetime2](7) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[KSBProgressStatusHistory] ADD PRIMARY KEY CLUSTERED 
(
    [KSBProgressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
