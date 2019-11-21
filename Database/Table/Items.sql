CREATE TABLE [dbo].[Items]
(
	[ItemId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(50) NOT NULL, 
    [Description] NCHAR(250) NULL, 
    [ShelfLife] DATE NOT NULL
)
