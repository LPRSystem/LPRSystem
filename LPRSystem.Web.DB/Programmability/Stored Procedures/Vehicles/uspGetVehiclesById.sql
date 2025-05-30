CREATE PROCEDURE [api].[uspGetVehiclesById]
  (
  @VehicleId   bigint
  )

  with RECOMPILE

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
      ,[modifiedOn]
      ,[IsActive]

 FROM [data].[Vehicles]

 Where VehicleId=@VehicleId

 END