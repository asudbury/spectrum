/****** Object:  Table [dbo].[Event]    Script Date: 30-Jan-17 9:03:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Event](
	[Guid] [uniqueidentifier] NOT NULL,
	[Event] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL DEFAULT (getdate()),
	[Description] [varchar](100),
	[Text] [varchar](100),
	FOREIGN KEY (Event) REFERENCES EventType(Id) ON DELETE CASCADE ON UPDATE CASCADE,
 CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO