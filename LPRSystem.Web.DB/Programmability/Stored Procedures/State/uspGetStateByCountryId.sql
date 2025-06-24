CREATE PROCEDURE [api].[uspGetStateByCountryId]
(
   @CountryId bigint
)

WITH RECOMPILE 

AS 
   BEGIN
     
     SELECT 
        sts.StateId,
		sts.CountryId, 
		ctry.Name as CountryName,
		ctry.CountryCode,
		sts.Name,
		sts.Description, 
		sts.StateCode,
		sts.CreatedOn,
		sts.CreatedBy,
		sts.ModifiedOn,
		sts.ModifiedBy,
		sts.IsActive
       FROM [data].[State] sts
       left join [data].[Country] ctry on sts.CountryId = ctry.CountryId
      WHERE ctry.CountryId = @CountryId
END


     