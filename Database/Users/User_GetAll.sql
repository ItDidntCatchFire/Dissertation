﻿DELIMITER //
DROP PROCEDURE IF EXISTS User_GetAll;
CREATE PROCEDURE User_GetAll()
BEGIN
	SELECT 
		UserId,
        Role
	FROM Users;
END//
DELIMITER ;