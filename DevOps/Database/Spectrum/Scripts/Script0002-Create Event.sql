/****** Object:  Table [dbo].[Event]    Script Date: 28-Jan-17 4:25:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Event](
	[Guid] [uniqueidentifier] NOT NULL,
	[Event] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO