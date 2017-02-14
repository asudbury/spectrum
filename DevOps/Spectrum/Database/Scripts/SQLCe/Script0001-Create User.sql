CREATE TABLE [User](
	[Name] [nvarchar](100) NOT NULL,
	[EmailAddress] [nvarchar](100) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[authenticated] [bit] NOT NULL DEFAULT ((0)),
	[created_date] [datetime] NOT NULL DEFAULT (getdate()),
	[updated_date] [datetime] NULL
) 

