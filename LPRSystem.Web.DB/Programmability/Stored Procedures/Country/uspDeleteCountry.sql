CREATE PROCEDURE [api].[uspDeleteCountry]
(@countryId bigint)
AS
	BEGIN
	update [data].[Country]
	 set  IsActive = 0
	WHERE CountryId=@countryId
END

