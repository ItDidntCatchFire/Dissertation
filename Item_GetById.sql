DELIMITER //
DROP PROCEDURE IF EXISTS Item_GetById//
CREATE PROCEDURE Item_GetById (IN itemId CHAR(36) binary) 
BEGIN
	SELECT 
		ItemId,
		Name,
		Description,
		ShelfLife,
		BuyPrice,
		SellPrice
	FROM Items
    WHERE ItemId = itemId;
END //
DELIMITER ;