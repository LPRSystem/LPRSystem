CREATE PROCEDURE [api].[uspDeleteOrganization]
(
@id bigint
)
AS
	BEGIN
	DELETE FROM [data].[Organization]

	WHERE Id=@id
END