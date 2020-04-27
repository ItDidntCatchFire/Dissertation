DELIMITER //
DROP PROCEDURE IF EXISTS User_Update //
CREATE PROCEDURE User_Update (IN userId CHAR(36) binary, IN role int) 
BEGIN
	SET SQL_SAFE_UPDATES = 0;
	UPDATE Users
    SET 
		Role = role
    WHERE UserId = userId;
END //
DELIMITER ;