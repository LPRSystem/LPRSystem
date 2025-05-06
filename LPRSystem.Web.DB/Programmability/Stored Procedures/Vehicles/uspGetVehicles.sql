CREATE PROCEDURE [api].[uspGetVehicles]
	
WITH RECOMPILE

AS 

BEGIN

SELECT
   
     [VehicleId]
    ,[VehicleName]
    ,
