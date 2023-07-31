CREATE PROCEDURE insertCategory
  @CategoryName VARCHAR(100),
  @Description TEXT,
  @CreationDate DATE
AS
BEGIN
  INSERT INTO Categories (CategoryName, Description, CreationDate)
  VALUES (@CategoryName, @Description, @CreationDate)
END;

CREATE PROCEDURE updateCategory
  @CategoryID INT,
  @CategoryName VARCHAR(100),
  @Description TEXT,
  @CreationDate DATE
AS
BEGIN
  UPDATE Categories
  SET CategoryName = @CategoryName,
      Description = @Description,
      CreationDate = @CreationDate
  WHERE CategoryID = @CategoryID
END;

CREATE PROCEDURE deleteCategory
  @CategoryID INT
AS
BEGIN
  DELETE FROM Categories
  WHERE CategoryID = @CategoryID
END;

CREATE PROCEDURE getCategoryByID
  @CategoryID INT
AS
BEGIN
  SELECT CategoryID, CategoryName, Description, CreationDate
  FROM Categories
  WHERE CategoryID = @CategoryID
END;

CREATE PROCEDURE getAllCategories
AS
BEGIN
    SELECT CategoryID, CategoryName, Description, CreationDate
    FROM Categories;
END;
