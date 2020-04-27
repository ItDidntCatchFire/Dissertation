DELIMITER //
DROP PROCEDURE IF EXISTS User_Insert//
CREATE PROCEDURE User_Insert (IN userId CHAR(36) binary, IN role int) 
BEGIN
	INSERT INTO Users (UserId, Role)
    VALUES(userId, role);
END //
DELIMITER ;