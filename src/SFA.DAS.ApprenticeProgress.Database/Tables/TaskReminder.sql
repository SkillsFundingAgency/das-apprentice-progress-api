﻿CREATE TABLE [dbo].[TaskReminder](
    [ReminderId] [int] IDENTITY(1,1) NOT NULL,
    [TaskId] [int] NOT NULL,
    [ReminderValue] [int] NULL,
    [ReminderUnit] [int] NULL,
    [Status] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TaskReminder] ADD  CONSTRAINT [PK_TaskReminder] PRIMARY KEY CLUSTERED 
(
    [ReminderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
