CREATE PROCEDURE [dbo].[spUser_Delete]
	@Id int
AS
	DELETE
	FROM  [dbo].[User] 
	WHERE [Id] = @Id
RETURN 0
