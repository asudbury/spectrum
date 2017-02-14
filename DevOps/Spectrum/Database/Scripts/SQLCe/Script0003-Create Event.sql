CREATE TABLE [Event](
	[Guid] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[Event] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL DEFAULT (getdate()),
	[Description] [nvarchar](100),
	[Text] [nvarchar](100),
	FOREIGN KEY (Event) REFERENCES EventType(Id) ON DELETE CASCADE ON UPDATE CASCADE
 )