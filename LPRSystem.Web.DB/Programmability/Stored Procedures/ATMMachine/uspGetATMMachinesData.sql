CREATE PROCEDURE [api].[uspGetATMMachinesData]

WITH RECOMPILE

AS

BEGIN

SELECT
	atm.[ATMId]
   ,atm.[ATMCode]
   ,atm.[LocationId]
   ,loc.[LocationName]
   ,atm.[CreatedOn]
   ,atm.[CreatedBy]
   ,atm.[ModifiedOn]
   ,atm.[ModifiedBy]
   ,atm.[IsActive]
From [data].[ATMMachine] atm
left join [data].[Location] loc on atm.LocationId = loc.LocationId

END
