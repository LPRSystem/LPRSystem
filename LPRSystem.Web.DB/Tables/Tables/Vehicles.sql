CREATE TABLE [data].[Vehicles]
(
	[VehicleId]        Int              NOT NULL   Primary Key   IDENTITY(1,1),
	[VehiclesName]     varchar(max)     NULL,
	[VehicleType]      varchar(maX)     NULL,
	[LicencePlate]     varchar(max)     NULL,
	[OwnerName]        varchar(max)     NULL,
	[ContactNumber]    varchar(30)      NULL,
	[CreatedBy]        bigint           NULL,
	[CreatedOn]        datetimeoffset   NULL,
	[ModifiedBy]       bigint           NULL,
	[ModifiedOn]       datetimeoffset   NULL,
	[IsActive]         bit              NULL
)
