CREATE PROCEDURE getPostsWithComments
AS
BEGIN
  SELECT P.PostID, P.UserID, P.CategoryID, P.Title, P.Content, P.Timestamp,
         C.CommentID, C.UserID AS CommentUserID, C.CommentText, C.Timestamp AS CommentTimestamp
  FROM Posts P
  LEFT JOIN Comments C ON P.PostID = C.PostID
  ORDER BY P.Timestamp DESC, C.Timestamp DESC;
END;

CREATE PROCEDURE getPostsByCategory
  @CategoryID INT
AS
BEGIN
  SELECT PostID, UserID, CategoryID, Title, Content, Timestamp
  FROM Posts
  WHERE CategoryID = @CategoryID
  ORDER BY Timestamp DESC;
END;

CREATE PROCEDURE getPostsByUser
  @UserID INT
AS
BEGIN
  SELECT PostID, UserID, CategoryID, Title, Content, Timestamp
  FROM Posts
  WHERE UserID = @UserID
  ORDER BY Timestamp DESC;
END;
