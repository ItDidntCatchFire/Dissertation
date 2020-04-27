DELIMITER //
DROP PROCEDURE IF EXISTS Inventory_GetById//
CREATE PROCEDURE Inventory_GetById (IN inventoryId CHAR(36) binary) 
BEGIN
	SELECT 
		InventoryId,
		ItemId,
		Quantity,
		Time,
		Export,
		Monies
	FROM Inventory
    WHERE InventoryId = inventoryId;
END //
DELIMITER ;