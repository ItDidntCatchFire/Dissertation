DELIMITER //
DROP PROCEDURE IF EXISTS Inventory_Update //
CREATE PROCEDURE Inventory_Update (IN pInventoryId CHAR(36) binary, IN pItemId CHAR(36) binary, IN pQuantity int, IN pTime datetime, IN pExport tinyint(1), IN pMonies decimal(5, 2)) 
BEGIN
	UPDATE Inventory
    SET 
		Quantity = pquantity,
		Time = pTime,
		Export = pExport,
		Monies = pMonies
    WHERE InventoryId = pInventoryId
	AND ItemId = pItemId;
END //
DELIMITER ;