DELIMITER //
DROP PROCEDURE IF EXISTS User_Update //
CREATE PROCEDURE User_Update (IN pUserId CHAR(36) binary, IN pRole int) 
BEGIN
	UPDATE Users
    SET 
		Role = pRole
    WHERE UserId = pUserId;
END //
DELIMITER ;