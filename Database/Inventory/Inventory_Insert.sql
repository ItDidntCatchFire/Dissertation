DELIMITER //
DROP PROCEDURE IF EXISTS Inventory_Insert//
CREATE PROCEDURE Inventory_Insert (IN pInventoryId CHAR(36) binary, IN pItemId CHAR(36) binary, IN pQuantity int, IN pTime datetime, IN pExport tinyint(1), IN pMonies decimal(5, 2)) 
BEGIN
	INSERT INTO Inventory (InventoryId, ItemId, Quantity, Time, Export, Monies)
    VALUES(pInventoryId, pItemId, pQuantity, pTime, pExport, pMonies);
END //
DELIMITER ;