/*
   24 June 201610:53:01
   User: 
   Server: .
   Database: lookatmyskateboard
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Skateboard SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.[Like]
	(
	UserId int NOT NULL,
	SkateboardId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.[Like] ADD CONSTRAINT
	PK_Like PRIMARY KEY CLUSTERED 
	(
	UserId,
	SkateboardId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.[Like] ADD CONSTRAINT
	FK_Like_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.[Like] ADD CONSTRAINT
	FK_Like_Skateboard FOREIGN KEY
	(
	SkateboardId
	) REFERENCES dbo.Skateboard
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.[Like] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
