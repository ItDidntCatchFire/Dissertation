# Users
CALL User_Insert("0f8fad5b-d9cb-469f-a165-70867728950e", 2);
CALL User_GetAll();
CALL User_Update("0f8fad5b-d9cb-469f-a165-70867728950e", 1);
CALL User_GetById("0f8fad5b-d9cb-469f-a165-70867728950e");

# Items
CALL Item_Insert("0f8fad5b-d9cb-469f-a165-70867728950e", "Stella", "Fruity", 1.00, 5.00, 365);
CALL Item_Insert("eaa0ec62-7e0d-454c-966a-171cbb17b0a1", "Guinness", "dark", 1.50, 5.00, 365);
CALL Item_GetAll();
CALL Item_Update("0f8fad5b-d9cb-469f-a165-70867728950e", "Stella", "Fruity", 1.00, 2.00, 365);
CALL Item_GetById("0f8fad5b-d9cb-469f-a165-70867728950e");

# Inventory
CALL Inventory_Insert("5b078b5a-d987-4424-88ea-57f2cca2866e", "0f8fad5b-d9cb-469f-a165-70867728950e", 100, now(), true, 1);
CALL Inventory_Insert("5b078b5a-d987-4424-88ea-57f2cca2866e", "eaa0ec62-7e0d-454c-966a-171cbb17b0a1", 1, now(), true, 2);
CALL Inventory_Insert("4da698cc-11a3-4e17-96b1-d3b99c027225", "0f8fad5b-d9cb-469f-a165-70867728950e", 5, now(), true, 1);
CALL Inventory_GetAll();
CALL Inventory_Update("5b078b5a-d987-4424-88ea-57f2cca2866e", "0f8fad5b-d9cb-469f-a165-70867728950e", 5, now(), true, 1);
CALL Inventory_GetById("5b078b5a-d987-4424-88ea-57f2cca2866e");




