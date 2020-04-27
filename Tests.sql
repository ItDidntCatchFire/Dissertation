CALL User_Insert("0f8fad5b-d9cb-469f-a165-70867728950e", 1);
CALL User_GetAll();
CALL User_Update("0f8fad5b-d9cb-469f-a165-70867728950e", 2);
CALL User_GetById("0f8fad5b-d9cb-469f-a165-70867728950e");

CALL Item_Insert("0f8fad5b-d9cb-469f-a165-70867728950e", "Test Name", "Test Description", 100.00, 200.00, 1);
CALL Item_GetAll();
CALL Item_Update("0f8fad5b-d9cb-469f-a165-70867728950e", "Stella", "Fruity", 1.00, 2.00, 365);
CALL Item_GetById("0f8fad5b-d9cb-469f-a165-70867728950e");

CALL Inventory_Insert("5b078b5a-d987-4424-88ea-57f2cca2866e", "0f8fad5b-d9cb-469f-a165-70867728950e", 5, now(), true, 1);
CALL Inventory_GetAll();
CALL Inventory_Update("5b078b5a-d987-4424-88ea-57f2cca2866e", "0f8fad5b-d9cb-469f-a165-70867728950e", 8, now(), true, 1);
CALL Inventory_GetById("0f8fad5b-d9cb-469f-a165-70867728950e");