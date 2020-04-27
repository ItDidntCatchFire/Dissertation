DELIMITER //
DROP PROCEDURE IF EXISTS Inventory_Insert//
CREATE PROCEDURE Inventory_Insert (IN InventoryId CHAR(36) binary, IN ItemId CHAR(36) binary, IN Quantity int, IN Time datetime, IN Export tinyint(1), IN Monies decimal(5, 2)) 
BEGIN
	INSERT INTO Inventory (InventoryId, ItemId, Quantity, Time, Export, Monies)
    VALUES(inventoryId, itemId, quantity, time, export, monies);
END //
DELIMITER ;