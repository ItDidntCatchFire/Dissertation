DELIMITER //
DROP PROCEDURE IF EXISTS Inventory_GetById//
CREATE PROCEDURE Inventory_GetById (IN pInventoryId CHAR(36) binary) 
BEGIN
	SELECT 
		InventoryId,
		ItemId,
		Quantity,
		Time,
		Export,
		Monies
	FROM Inventory
    WHERE InventoryId = pInventoryId;
END //
DELIMITER ;