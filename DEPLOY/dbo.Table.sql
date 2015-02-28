CREATE TABLE [dbo].[Table]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [JobTitle] VARCHAR(150) NOT NULL, 
    [CompanyName] VARCHAR(150) NOT NULL, 
    [JobDescription] VARCHAR(500) NULL, 
    [BusinessSector] VARCHAR(150) NULL, 
    [Postcode] VARCHAR(10) NOT NULL, 
    [Town] VARCHAR(50) NULL, 
    [County] VARCHAR(50) NULL, 
    [RecruiterName] VARCHAR(50) NULL, 
    [RecruiterNumber] VARCHAR(50) NULL, 
    [RecruiterEmail] VARCHAR(50) NULL, 
    [ApplicationNotes] VARCHAR(500) NULL 
)
