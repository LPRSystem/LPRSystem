CREATE PROCEDURE [api].[uspDeleteATMMachine]
(@ATMId bigint)
AS
	BEGIN
	DELETE  FROM [data].[ATMMachine]

	WHERE ATMId=@ATMId
END