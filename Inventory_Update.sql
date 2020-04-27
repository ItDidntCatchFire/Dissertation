DELIMITER //
DROP PROCEDURE IF EXISTS Inventory_Update //
CREATE PROCEDURE Inventory_Update (IN InventoryId CHAR(36) binary, IN ItemId CHAR(36) binary, IN Quantity int, IN Time datetime, IN Export tinyint(1), IN Monies decimal(5, 2)) 
BEGIN
	SET SQL_SAFE_UPDATES = 0;
	UPDATE Inventory
    SET 
		ItemId = itemId,
		Quantity = quantity,
		Time = time,
		Export = export,
		Monies = monies
    WHERE InventoryId = inventoryId;
END //
DELIMITER ;