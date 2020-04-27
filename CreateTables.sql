DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Items;
DROP TABLE IF EXISTS Inventory;

CREATE TABLE Users (
	UserId CHAR(16) binary,
    Role int,
		PRIMARY KEY (UserId)
);

CREATE TABLE Items (
	ItemId CHAR(16) binary,
	Name VARCHAR(50),
	Description VARCHAR(50),
	ShelfLife int,
	BuyPrice decimal(5, 2),
	SellPrice decimal(5, 2),
		PRIMARY KEY (ItemId)
);

CREATE TABLE Inventory (
	InventoryId CHAR(16) binary,
    ItemId CHAR(16) binary,
    Quantity int,
    Time datetime,
    Export tinyint(1),
    Monies decimal(5, 2),
		PRIMARY KEY (InventoryId),
        FOREIGN KEY (ItemId) REFERENCES Items(ItemId)
);

