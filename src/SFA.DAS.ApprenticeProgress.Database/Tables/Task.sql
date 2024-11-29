CREATE TABLE [dbo].[Task](
    [TaskId] [int] IDENTITY(1,1) NOT NULL,
    [ApprenticeshipId] [bigint] NOT NULL,
    [ApprenticeAccountId] [uniqueidentifier] NULL,
    [DueDate] [datetime2](7) NULL,
    [Title] [nvarchar](255) NULL,
    [ApprenticeshipCategoryId] [int] NULL,
    [Note] [nvarchar](max) NULL,
    [CompletionDateTime] [datetime2](7) NULL,
    [CreatedDateTime] [datetime2](7) NULL,
    [Status] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Task] ADD PRIMARY KEY CLUSTERED 
(
    [TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Task] ADD  DEFAULT (getdate()) FOR [CreatedDateTime]
GO