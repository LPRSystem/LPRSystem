CREATE PROCEDURE [dbo].[uspGetState]
	WITH RECOMPILE

AS

BEGIN

  SELECT
		[StateId]
		,[CountryId] 
		,[Name]
		,[Description] 
		,[StateCode]
		,[CreatedOn]
		,[CreatedBy]
		,[ModifiedOn]
		,[ModifiedBy]
		,[IsActive]
  FROM [data].[State]

END

