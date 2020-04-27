DELIMITER //
DROP PROCEDURE IF EXISTS Item_Insert//
CREATE PROCEDURE Item_Insert (IN pItemId CHAR(36) binary, IN pName VARCHAR(50), IN pDescription VARCHAR(50), IN pShelfLife int, IN pBuyPrice decimal(5, 2), IN pSellPrice decimal(5, 2)) 
BEGIN
	INSERT INTO Items (ItemId, Name, Description, ShelfLife, BuyPrice, SellPrice)
    VALUES (pItemId, pName, pDescription, pShelfLife, pBuyPrice, pSellPrice);
END //
DELIMITER ;