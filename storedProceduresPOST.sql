CREATE PROCEDURE insertPost
  @UserID INT,
  @CategoryID INT,
  @Title VARCHAR(100),
  @Content TEXT,
  @Timestamp DATETIME
AS
BEGIN
  INSERT INTO Posts (UserID, CategoryID, Title, Content, Timestamp)
  VALUES (@UserID, @CategoryID, @Title, @Content, @Timestamp)
END;

CREATE PROCEDURE updatePost
  @PostID INT,
  @UserID INT,
  @CategoryID INT,
  @Title VARCHAR(100),
  @Content TEXT,
  @Timestamp DATETIME
AS
BEGIN
  UPDATE Posts
  SET UserID = @UserID,
      CategoryID = @CategoryID,
      Title = @Title,
      Content = @Content,
      Timestamp = @Timestamp
  WHERE PostID = @PostID
END;

CREATE PROCEDURE deletePost
  @PostID INT
AS
BEGIN
  DELETE FROM Posts
  WHERE PostID = @PostID
END;

CREATE PROCEDURE getPostByID
  @PostID INT
AS
BEGIN
  SELECT PostID, UserID, CategoryID, Title, Content, Timestamp
  FROM Posts
  WHERE PostID = @PostID
END;

CREATE PROCEDURE getAllPosts
AS
BEGIN
    SELECT PostID, UserID, CategoryID, Title, Content, Timestamp
  FROM Posts;
END;
