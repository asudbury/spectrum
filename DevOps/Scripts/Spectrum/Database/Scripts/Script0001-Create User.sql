/****** Object:  Table [dbo].[User]    Script Date: 28-Jan-17 4:22:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[User](
	[Name] [varchar](100) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[authenticated] [bit] NOT NULL DEFAULT ((0)),
	[created_date] [datetime] NOT NULL DEFAULT (getdate()),
	[updated_date] [datetime] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO