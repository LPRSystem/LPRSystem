CREATE PROCEDURE [api].[uspDeleteVehicles]
	(@Vehicleid bigint)

AS

BEGIN

update [data].[Vehicles]
       set IsActive=0 
	   where VehicleId= @VehicleId

END
