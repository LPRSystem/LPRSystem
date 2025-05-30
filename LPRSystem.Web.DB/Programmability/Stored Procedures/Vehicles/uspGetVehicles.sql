CREATE PROCEDURE [api].[uspGetVehicles]
	
WITH RECOMPILE

AS 

BEGIN

SELECT
   
     [VehicleId]
    ,[VehiclesName]
    ,[VehicleType]
    ,[LicencePlate]
    ,[OwnerName]
    ,[ContactNumber]
    ,[CreatedBy]
    ,[CreatedOn]
    ,[ModifiedBy]
    ,[ModifiedOn]
    ,[IsActive]

    FROM [data].[Vehicles]

    END