DELIMITER //
DROP PROCEDURE IF EXISTS Item_GetById//
CREATE PROCEDURE Item_GetById (IN pItemId CHAR(36) binary) 
BEGIN
	SELECT 
		ItemId,
		Name,
		Description,
		ShelfLife,
		BuyPrice,
		SellPrice
	FROM Items
    WHERE ItemId = pItemId;
END //
DELIMITER ;