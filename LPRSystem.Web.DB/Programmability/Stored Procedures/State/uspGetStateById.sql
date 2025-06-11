CREATE PROCEDURE [api].[uspGetStateById]
(
@StateId bigint 
)
	WITH RECOMPILE

AS

BEGIN

  SELECT
		sts.StateId,
		sts.CountryId, 
		sts.Name,
		sts.Description,
		sts.StateCode,
		sts.CreatedOn,
		sts.CreatedBy,
		sts.ModifiedOn,
		sts.ModifiedBy,
		sts.IsActive
  FROM [data].[State] sts

  WHERE sts.StateId=@StateId
END

     