CREATE PROCEDURE [dbo].[InsertItem]
	@ItemJSON VARCHAR(MAX),
	@ItemId INT OUTPUT
AS
BEGIN

	INSERT INTO [dbo].Items (Name, Description)
	SELECT  
		Name,
		Description
	FROM OPENJSON(@ItemJSON)
	WITH (
		Name			NVARCHAR(MAX)	'strict $.Name',
		Description		NVARCHAR(MAX)	'strict $.Description'
		);

	SET @ItemId =  @@IDENTITY;
END