CREATE PROCEDURE [api].[uspInsertVehicles]
 (
   @VehiclesName    varchar(50),
   @VehicleType    varchar(50),
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

 INSERT INTO [data].[Vehicles]
                    (
                    VehiclesName,
                    VehicleType,
                    LicencePlate,
                    OwnerName,
                    ContactNumber,
                    CreatedBy,
                    CreatedOn,
                    ModifiedBy,
                    ModifiedOn,
                    IsActive
                    )
                    values
                    (
                     @VehiclesName,
                     @VehicleType,
                     @LicencePlate,
                     @OwnerName,
                     @ContactNumber,
                     @CreatedBy,
                     @CreatedOn,
                     @ModifiedBy,
                     @ModifiedOn,
                     @IsActive
                     )

          EXEC [api].[uspInsertVehicles]
END

