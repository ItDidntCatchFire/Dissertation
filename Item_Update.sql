DELIMITER //
DROP PROCEDURE IF EXISTS Item_Update //
CREATE PROCEDURE Item_Update (IN itemId CHAR(36) binary, IN name VARCHAR(50), IN description VARCHAR(50), IN shelfLife int, IN buyPrice decimal(5, 2), IN sellPrice decimal(5, 2)) 
BEGIN
	SET SQL_SAFE_UPDATES = 0;
	UPDATE Items
    SET 
		Name = name,
		Description = description,
		ShelfLife = shelfLife,
		BuyPrice = buyPrice,
		SellPrice = sellPrice
    WHERE ItemId = itemId;
END //
DELIMITER ;