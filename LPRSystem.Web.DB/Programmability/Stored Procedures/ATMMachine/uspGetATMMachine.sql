CREATE PROCEDURE [dbo].[uspGetATMMachine]

WITH RECOMPILE

AS

BEGIN

SELECT
	[ATMId]
   ,[MachineName]
   ,[BankName]
   ,[CashAvailable]
   ,[LocationId]
   ,[CreatedOn]
   ,[CreatedBy]
   ,[ModifiedOn]
   ,[ModifiedBy]
   ,[IsActive]
From [data].[ATMMachine]

END
