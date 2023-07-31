CREATE TABLE Posts (
  PostID INT PRIMARY KEY IDENTITY(1,1),
  UserID INT,
  CategoryID INT,
  Title VARCHAR(100),
  Content TEXT,
  Timestamp DATETIME,
  FOREIGN KEY (UserID) REFERENCES [User](UserID),
  FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
);

CREATE TABLE Comments (
  CommentID INT PRIMARY KEY IDENTITY(1,1),
  UserID INT,
  PostID INT,
  CommentText VARCHAR(500),
  Timestamp DATETIME,
  FOREIGN KEY (UserID) REFERENCES [User](UserID),
  FOREIGN KEY (PostID) REFERENCES Posts(PostID),
);

CREATE TABLE Categories (
  CategoryID INT PRIMARY KEY IDENTITY(1,1),
  CategoryName VARCHAR(100),
  Description TEXT,
  CreationDate DATE
);

CREATE TABLE [User] (
  UserID INT PRIMARY KEY IDENTITY(1,1),
  Email VARCHAR(255),
  Username VARCHAR(50),
  Password VARCHAR(255),
  Firstname VARCHAR(50),
  Lastname VARCHAR(50),
  DateOfBirth DATE,
);
