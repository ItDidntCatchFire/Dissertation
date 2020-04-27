DELIMITER //
DROP PROCEDURE IF EXISTS Item_Update //
CREATE PROCEDURE Item_Update (IN pItemId CHAR(36) binary, IN pName VARCHAR(50), IN pDescription VARCHAR(50), IN pShelfLife int, IN pBuyPrice decimal(5, 2), IN pSellPrice decimal(5, 2)) 
BEGIN
	UPDATE Items
    SET 
		Name = pName,
		Description = pDescription,
		ShelfLife = pShelfLife,
		BuyPrice = pBuyPrice,
		SellPrice = pSellPrice
    WHERE ItemId = pItemId;
END //
DELIMITER ;