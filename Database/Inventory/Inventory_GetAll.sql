DELIMITER //
DROP PROCEDURE IF EXISTS Inventory_GetAll;
CREATE PROCEDURE Inventory_GetAll()
BEGIN
	SELECT 
		InventoryId,
		ItemId,
		Quantity,
		Time,
		Export,
		Monies
	FROM Inventory;
END//
DELIMITER ;