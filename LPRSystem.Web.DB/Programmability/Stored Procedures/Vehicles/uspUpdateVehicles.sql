CREATE PROCEDURE [api].[uspUpdateVehicles]
	(@VehicleId      bigint,
	 @VehiclesName    varchar(max),
	 @VehicleType    varchar(max),
	 @LicencePlate   varchar(max),
	 @OwnerName      varchar(max),
	 @ContactNumber  bigint,
	 @CreatedBy      bigint,
	 @CreatedOn      datetimeoffset,
	 @ModifiedBy     bigint,
	 @ModifiedOn     datetimeoffset,
	 @IsActive       bit
	 )
	 AS
	 BEGIN
	 update [data].[Vehicles] set
	 VehiclesName = @VehiclesName,
     VehicleType  = @VehicleType,
	 LicencePlate = @LicencePlate,
	 OwnerName    = @OwnerName,
	 ContactNumber= @ContactNumber,
	 CreatedBy    = @CreatedBy,
	 CreatedOn    = @CreatedOn,
	 ModifiedBy   = @ModifiedBy,
	 ModifiedOn   = @ModifiedOn,
	 IsActive     = @IsActive

	 WHERE VehicleId=@VehicleId

	 END

	 EXEC  [api].[uspGetRoles]