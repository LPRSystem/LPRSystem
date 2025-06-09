CREATE PROCEDURE [api].[uspDeleteCountry]
(@CountryId bigint)
AS
	BEGIN
	DELETE  FROM [data].[Country]

	WHERE CountryId=@CountryId
END
