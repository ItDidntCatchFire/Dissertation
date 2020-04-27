DELIMITER //
DROP PROCEDURE IF EXISTS Item_GetAll;
CREATE PROCEDURE Item_GetAll()
BEGIN
	SELECT 
		ItemId,
		Name,
		Description,
		ShelfLife,
		BuyPrice,
		SellPrice
	FROM Items;
END//
DELIMITER ;