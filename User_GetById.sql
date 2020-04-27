﻿DELIMITER //
DROP PROCEDURE IF EXISTS User_GetById//
CREATE PROCEDURE User_GetById (IN userId CHAR(36) binary) 
BEGIN
	SELECT 
		UserId,
		Role
	FROM Users
    WHERE UserId = userId;
END //
DELIMITER ;