


CREATE OR ALTER PROCEDURE sp_getbook
AS
BEGIN
	SELECT BookId, 
		   BookTitle, 
		   BookDescr,
		   b.AuthorId, 
		   AuthorName,
		   BookCategory,
		   BookPublishDate,
		   FilePath
	FROM   Book b
	INNER JOIN Author a ON (b.AuthorId = a.AuthorId)
	ORDER BY BookId desc; 
END


GO

CREATE OR ALTER PROCEDURE sp_getbookbyID
	@vBookId	int
AS
BEGIN
	SELECT BookId, 
		   BookTitle, 
		   BookDescr,
		   b.AuthorId, 
		   AuthorName,
		   BookCategory,
		   BookPublishDate,
		   FilePath 
	FROM   Book b 
	INNER JOIN Author a ON (b.AuthorId = a.AuthorId) 
	WHERE  BookId = @vBookId;
END


GO

CREATE OR ALTER PROCEDURE sp_insertBook
	@vBookTitle       varchar(50),
	@vBookDescr       varchar(100),
	@vAuthorId        int,
	@vBookCategory    varchar(50),
	@vBookPublishDate date,
	@vFilePath		  varchar(200)
AS
BEGIN
	INSERT INTO Book 
	( 
		BookTitle,
		BookDescr,
		AuthorId,
		BookCategory,
		BookPublishDate,
		FilePath
	)
	VALUES
	(
		@vBookTitle,
		@vBookDescr,
		@vAuthorId,
		@vBookCategory,
		@vBookPublishDate,
		@vFilePath
	);
END



GO


CREATE OR ALTER PROCEDURE sp_updateBook
	@vBookId	      int,
	@vBookTitle       varchar(50),
	@vBookDescr       varchar(100),
	@vAuthorId        int,
	@vBookCategory    varchar(50),
	@vBookPublishDate date,
	@vFilePath		  varchar(200)
AS
BEGIN
	UPDATE Book 
	SET
		   BookTitle       = @vBookTitle,
		   BookDescr	   = @vBookDescr,
		   AuthorId		   = @vAuthorId,
		   BookCategory	   = @vBookCategory,
		   BookPublishDate = @vBookPublishDate,
		   FilePath		   = @vFilePath
	WHERE  BookId          = @vBookId   
END

GO

CREATE OR ALTER PROCEDURE sp_deletebookbyID
	@vBookId	int
AS
BEGIN
	DELETE FROM   Book
	WHERE  BookId = @vBookId;
END


GO


CREATE OR ALTER PROCEDURE sp_getAuthor
AS
BEGIN
	SELECT AuthorId, 
		   AuthorName, 
		   AuthorLocation, 
		   AuthorDesc
	FROM   Author;
END


GO

CREATE OR ALTER PROCEDURE sp_getAuthorbyID
	@vAuthorId	int
AS
BEGIN
	SELECT AuthorId, 
		   AuthorName, 
		   AuthorLocation, 
		   AuthorDesc
	FROM   Author
	WHERE  AuthorId = @vAuthorId;
END


GO

CREATE OR ALTER PROCEDURE sp_insertAuthor
	@vAuthorName       varchar(50),
	@vAuthorLocation   varchar(50),
	@vAuthorDesc       varchar(500)
AS
BEGIN
	INSERT INTO Author 
	( 
		AuthorName,
		AuthorLocation,
		AuthorDesc
	)
	VALUES
	(
		@vAuthorName,
		@vAuthorLocation,
		@vAuthorDesc
	);
END



GO


CREATE OR ALTER PROCEDURE sp_updateAuthor
	@vAuthorId	       int,
	@vAuthorName       varchar(50),
	@vAuthorLocation   varchar(50),
	@vAuthorDesc       varchar(500)
AS
BEGIN
	UPDATE Author 
	SET
		   AuthorName      = @vAuthorName,
		   AuthorLocation  = @vAuthorLocation,
		   AuthorDesc	   = @vAuthorDesc
	WHERE  AuthorId        = @vAuthorId   
END

GO

CREATE OR ALTER PROCEDURE sp_deleteAuthorbyID
	@vAuthorId	int
AS
BEGIN
	DELETE FROM   Author
	WHERE  AuthorId = @vAuthorId;
END