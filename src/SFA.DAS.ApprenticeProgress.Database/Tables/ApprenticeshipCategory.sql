SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApprenticeshipCategory](
    [CategoryId] [int] NOT NULL,
    [ApprenticeshipId] [uniqueidentifier] NULL,
    [Title] [nvarchar](255) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApprenticeshipCategory] ADD PRIMARY KEY CLUSTERED 
(
    [CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
