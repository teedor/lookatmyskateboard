/*
   13 June 201609:49:00
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
ALTER TABLE dbo.Skateboard
	DROP CONSTRAINT FK_Skateboard_Users
GO
ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Skateboard ADD CONSTRAINT
	FK_Skateboard_Users FOREIGN KEY
	(
	uploadedBy
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Skateboard SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Comment
	(
	id int NOT NULL IDENTITY (1, 1),
	Text nvarchar(1000) NOT NULL,
	skateboardId int NOT NULL,
	UserId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Comment ADD CONSTRAINT
	PK_Comment PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Comment ADD CONSTRAINT
	FK_Comment_Skateboard FOREIGN KEY
	(
	skateboardId
	) REFERENCES dbo.Skateboard
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Comment ADD CONSTRAINT
	FK_Comment_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Comment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
