CREATE PROCEDURE insertUser
  @Email VARCHAR(255),
  @Username VARCHAR(50),
  @Password VARCHAR(255),
  @Firstname VARCHAR(50),
  @Lastname VARCHAR(50),
  @DateOfBirth DATE
AS
BEGIN
  INSERT INTO [User] (Email, Username, Password, Firstname, Lastname, DateOfBirth)
  VALUES (@Email, @Username, @Password, @Firstname, @Lastname, @DateOfBirth)
END;

CREATE PROCEDURE updateUser
  @UserID INT,
  @Email VARCHAR(255),
  @Username VARCHAR(50),
  @Password VARCHAR(255),
  @Firstname VARCHAR(50),
  @Lastname VARCHAR(50),
  @DateOfBirth DATE
AS
BEGIN
  UPDATE [User]
  SET Email = @Email,
      Username = @Username,
      Password = @Password,
      Firstname = @Firstname,
      Lastname = @Lastname,
      DateOfBirth = @DateOfBirth
  WHERE UserID = @UserID
END;

CREATE PROCEDURE deleteUser
  @UserID INT
AS
BEGIN
  DELETE FROM [User]
  WHERE UserID = @UserID
END;

CREATE PROCEDURE getUserByID
  @UserID INT
AS
BEGIN
  SELECT UserID, Email, Username, Firstname, Lastname, DateOfBirth
  FROM [User]
  WHERE UserID = @UserID
END;

CREATE PROCEDURE getAllUsers
AS
BEGIN
    SELECT UserID, Email, Username, Firstname, Lastname, DateOfBirth
  FROM [User];
END;

CREATE PROCEDURE checkUserExistence
  @Username VARCHAR(50),
  @Email VARCHAR(255)
AS
BEGIN
  IF EXISTS (SELECT 1 FROM [User] WHERE Username = @Username OR Email = @Email)
  BEGIN
    SELECT 1 AS 'UserExists'
  END
  ELSE
  BEGIN
    SELECT 0 AS 'UserExists'
  END
END;

CREATE PROCEDURE loginUser
  @Username VARCHAR(50),
  @Password VARCHAR(255),
  @LoginStatus BIT OUTPUT,
  @UserID INT OUTPUT
AS
BEGIN
  IF EXISTS (SELECT 1 FROM [User] WHERE Username = @Username AND Password = @Password)
  BEGIN
    SET @LoginStatus = 1; -- 1 for successful login
    SELECT @UserID = UserID FROM [User] WHERE Username = @Username;
  END
  ELSE
  BEGIN
    SET @LoginStatus = 0; -- 0 for failed login
    SET @UserID = NULL;
  END
END;

