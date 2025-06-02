CREATE PROCEDURE [api].[uspGetATMMachine]

WITH RECOMPILE

AS

BEGIN

SELECT
	[ATMId]
   ,[ATMCode]
   ,[LocationId]
   ,[CreatedOn]
   ,[CreatedBy]
   ,[ModifiedOn]
   ,[ModifiedBy]
   ,[IsActive]
From [data].[ATMMachine]

END
