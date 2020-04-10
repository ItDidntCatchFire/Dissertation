CREATE PROCEDURE [dbo].[GetItemByItemId]
	@ItemId INT
AS
BEGIN
	SELECT 
		ItemId,
		Name,
		Description 
	FROM 
		[dbo].Items
	WHERE 
		Items.ItemId = @ItemId
END