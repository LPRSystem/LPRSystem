CREATE PROCEDURE [api].[uspGetCountryById]
(
 @CountryId  bigint
)
WITH RECOMPILE

AS 

BEGIN

SELECT

     [CountryId]
    ,[Name]
    ,[Description]
    ,[CountryCode]
    ,[CreatedBy]
    ,[CreatedOn]
    ,[ModifiedBy]
    ,[ModifiedOn]
    ,[IsActive]

    FROM [data].[Country]

    WHERE CountryId=@CountryId

    END
