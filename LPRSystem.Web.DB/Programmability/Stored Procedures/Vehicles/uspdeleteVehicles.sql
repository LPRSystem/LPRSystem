CREATE PROCEDURE [api].[uspDeleteVehicles]
	(@Vehiclesid bigint)

AS

BEGIN

update [data].[Vehicles]
       set IsActive=0 
	   where VehicleId= @VehicleId

END
