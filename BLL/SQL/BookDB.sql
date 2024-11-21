USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BooksDB')
BEGIN
    ALTER DATABASE BooksDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BooksDB;
END
GO

-- Create the database
CREATE DATABASE BooksDB;
GO

-- Use the created database
USE BooksDB;
GO

-- Create Author table
CREATE TABLE Author (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Surname NVARCHAR(255) NOT NULL
);
GO

-- Create Genre table
CREATE TABLE Genre (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL
);
GO

-- Create Role table
CREATE TABLE Role (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL
);
GO

-- Create User table
CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    RoleId INT NOT NULL,
    CONSTRAINT FK_User_Role FOREIGN KEY (RoleId) REFERENCES Role(Id)
);
GO

-- Create Book table
CREATE TABLE Book (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    NumberOfPages SMALLINT NULL,
    PublishDate DATETIME NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    IsTopSeller BIT NOT NULL DEFAULT 0,
    AuthorId INT NOT NULL,
    CONSTRAINT FK_Book_Author FOREIGN KEY (AuthorId) REFERENCES Author(Id)
);
GO

-- Create BookGenre table
CREATE TABLE BookGenre (
    Id INT PRIMARY KEY IDENTITY(1,1),
    BookId INT NOT NULL,
    GenreId INT NOT NULL,
    CONSTRAINT FK_BookGenre_Book FOREIGN KEY (BookId) REFERENCES Book(Id),
    CONSTRAINT FK_BookGenre_Genre FOREIGN KEY (GenreId) REFERENCES Genre(Id)
);
GO

-- Insert data into Author table
INSERT INTO Author (Name, Surname)
VALUES 
('George', 'Orwell'),
('J.K.', 'Rowling'),
('F. Scott', 'Fitzgerald'),
('J.R.R.', 'Tolkien'),
('Harper', 'Lee'),
('Jane', 'Austen'),
('Mark', 'Twain'),
('Mary', 'Shelley'),
('Ernest', 'Hemingway'),
('John', 'Steinbeck'),
('Agatha', 'Christie'),
('Isaac', 'Asimov'),
('Arthur', 'Clarke'),
('C.S.', 'Lewis'),
('Leo', 'Tolstoy'),
('Victor', 'Hugo'),
('Charlotte', 'Bronte'),
('Herman', 'Melville'),
('Franz', 'Kafka'),
('Gabriel', 'Garcia Marquez'),
('Azra', 'Kohen'),
('Adam', 'Fawer');
GO

-- Insert data into Genre table
INSERT INTO Genre (Name)
VALUES 
('Fiction'),
('Fantasy'),
('Classics'),
('Science Fiction'),
('Mystery'),
('Romance'),
('Adventure'),
('Horror'),
('Historical Fiction'),
('Drama');
GO

-- Insert data into Book table
INSERT INTO Book (Name, NumberOfPages, PublishDate, Price, IsTopSeller, AuthorId)
VALUES 
('1984', 328, '1949-06-08', 15.99, 1, 1),
('Harry Potter and the Chamber of Secrets', 341, '1998-07-02', 19.99, 1, 2),
('Pride and Prejudice', 279, '1813-01-28', 10.99, 1, 6),
('The Adventures of Tom Sawyer', 274, '1876-06-01', 9.99, 0, 7),
('Frankenstein', 280, '1818-01-01', 14.99, 1, 8),
('The Old Man and the Sea', 127, '1952-09-01', 12.99, 1, 9),
('The Grapes of Wrath', 464, '1939-04-14', 18.99, 0, 10),
('Murder on the Orient Express', 256, '1934-01-01', 16.99, 1, 11),
('Foundation', 244, '1951-06-01', 14.99, 1, 12),
('2001: A Space Odyssey', 297, '1968-07-01', 15.99, 1, 13),
('The Lion, the Witch and the Wardrobe', 208, '1950-10-16', 13.99, 1, 14),
('War and Peace', 1225, '1869-01-01', 20.99, 0, 15),
('Les Misérables', 1463, '1862-01-01', 22.99, 1, 16),
('Jane Eyre', 532, '1847-10-16', 12.99, 1, 17),
('Moby Dick', 635, '1851-10-18', 17.99, 0, 18),
('The Metamorphosis', 201, '1915-10-01', 10.99, 1, 19),
('One Hundred Years of Solitude', 417, '1967-06-05', 14.99, 1, 20),
('To Kill a Mockingbird', 281, '1960-07-11', 12.99, 1, 5),
('The Hobbit', 310, '1937-09-21', 14.99, 1, 4),
('The Great Gatsby', 180, '1925-04-10', 10.99, 1, 3);
GO

-- Insert data into BookGenre table
INSERT INTO BookGenre (BookId, GenreId)
VALUES 
(1, 1),  -- 1984, Fiction
(2, 2),  -- Harry Potter, Fantasy
(3, 6),  -- Pride and Prejudice, Romance
(4, 7),  -- The Adventures of Tom Sawyer, Adventure
(5, 8),  -- Frankenstein, Horror
(6, 1),  -- The Old Man and the Sea, Fiction
(7, 9),  -- The Grapes of Wrath, Historical Fiction
(8, 5),  -- Murder on the Orient Express, Mystery
(9, 4),  -- Foundation, Science Fiction
(10, 4), -- 2001: A Space Odyssey, Science Fiction
(11, 2), -- The Lion, the Witch and the Wardrobe, Fantasy
(12, 9), -- War and Peace, Historical Fiction
(13, 9), -- Les Misérables, Historical Fiction
(14, 6), -- Jane Eyre, Romance
(15, 7), -- Moby Dick, Adventure
(16, 1), -- The Metamorphosis, Fiction
(17, 9), -- One Hundred Years of Solitude, Historical Fiction
(18, 1), -- To Kill a Mockingbird, Fiction
(19, 2), -- The Hobbit, Fantasy
(20, 3); -- The Great Gatsby, Classics
GO
