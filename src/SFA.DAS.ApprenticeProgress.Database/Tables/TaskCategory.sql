﻿CREATE TABLE [dbo].[TaskCategory](
    [TaskId] [int] NOT NULL,
    [CategoryId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TaskCategory] ADD  CONSTRAINT [PK_TaskCategory] PRIMARY KEY CLUSTERED 
(
    [TaskId] ASC,
    [CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
