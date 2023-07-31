CREATE PROCEDURE insertComment
  @UserID INT,
  @PostID INT,
  @CommentText VARCHAR(500),
  @Timestamp DATETIME
AS
BEGIN
  INSERT INTO Comments (UserID, PostID, CommentText, Timestamp)
  VALUES (@UserID, @PostID, @CommentText, @Timestamp)
END;

CREATE PROCEDURE updateComment
  @CommentID INT,
  @UserID INT,
  @PostID INT,
  @CommentText VARCHAR(500),
  @Timestamp DATETIME
AS
BEGIN
  UPDATE Comments
  SET UserID = @UserID,
      PostID = @PostID,
      CommentText = @CommentText,
      Timestamp = @Timestamp
  WHERE CommentID = @CommentID
END;

CREATE PROCEDURE deleteComment
  @CommentID INT
AS
BEGIN
  DELETE FROM Comments
  WHERE CommentID = @CommentID
END;

CREATE PROCEDURE getCommentsByPostID
  @PostID INT
AS	
BEGIN
  SELECT CommentID, PostID, CommentText,UserID ,Timestamp
  FROM Comments
  WHERE PostID = @PostID;
END;

