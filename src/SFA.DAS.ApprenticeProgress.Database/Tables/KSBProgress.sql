CREATE TABLE [dbo].[KSBProgress](
    [KSBProgressId] [int] IDENTITY(1,1) NOT NULL,
    [ApprenticeshipId] [uniqueidentifier] NOT NULL,
    [KSBProgressType] [int] NOT NULL,
    [KSBId] [uniqueidentifier] NOT NULL,
    [KSBKey] [nvarchar](max) NOT NULL,
    [CurrentStatus] [int] NOT NULL,
    [Note] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[KSBProgress] ADD PRIMARY KEY CLUSTERED 
(
    [KSBProgressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
