﻿CREATE TABLE [dbo].[TaskFile](
    [TaskFileId] [int] IDENTITY(1,1) NOT NULL,
    [TaskId] [int] NOT NULL,
    [FileName] [nvarchar](max) NULL,
    [FileType] [nvarchar](max) NULL,
    [FileContents] [varbinary](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TaskFile] ADD PRIMARY KEY CLUSTERED 
(
    [TaskFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
