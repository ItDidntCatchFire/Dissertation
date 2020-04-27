DELIMITER //
DROP PROCEDURE IF EXISTS Item_Insert//
CREATE PROCEDURE Item_Insert (IN itemId CHAR(36) binary, IN name VARCHAR(50), IN description VARCHAR(50), IN shelfLife int, IN buyPrice decimal(5, 2), IN sellPrice decimal(5, 2)) 
BEGIN
	INSERT INTO Items (ItemId, Name, Description, ShelfLife, BuyPrice, SellPrice)
    VALUES(itemId, name, description, shelfLife, buyPrice, sellPrice);
END //
DELIMITER ;