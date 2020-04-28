DELIMITER //
DROP PROCEDURE IF EXISTS User_Insert//
CREATE PROCEDURE User_Insert (IN pUserId CHAR(36) binary, IN pRole int) 
BEGIN
	INSERT INTO Users (UserId, Role)
    VALUES(pUserId, pRole);
END //
DELIMITER ;