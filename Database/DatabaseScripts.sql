CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](25) NOT NULL,
	[FullName] [varchar](50) NULL,
CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)
)
	GO

CREATE TABLE Certifications (
[CertificationId] [int] IDENTITY(1,1) NOT NULL,
[Name] Varchar(400) Not NULL,
[IssuedBy]  Varchar(400) Not NULL,
[InsertedDate] DateTime Not Null,
 Active Bit Not Null,
 CONSTRAINT [PK_Certifications] PRIMARY KEY CLUSTERED 
(
	[CertificationId] ASC
)

)
GO

Create Table UsersCertifications
(UserId int Not Null,
CertificationId int Not Null,
CertificationDate DateTime NOT NULL
)
GO
ALTER TABLE UsersCertifications
ADD  CONSTRAINT [FK_UsersCertifications_Users_UserId]
FOREIGN KEY (UserId) REFERENCES [Users](UserId);
GO
GO
ALTER TABLE UsersCertifications
ADD  CONSTRAINT [FK_UsersCertifications_Certifications_CertificationId]
 FOREIGN KEY (CertificationId) REFERENCES Certifications(CertificationId);
