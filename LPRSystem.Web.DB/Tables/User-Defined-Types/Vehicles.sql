CREATE TYPE [api].[Vehicles] as Table (
    [VehicleId]          bigint             NULL,
    [VehiclesName]       varchar(max)       NULL,
    [VehiclesType]       varchar(max)       NULL,
    [LicencePlate]       varchar(max)       NULL,
    [OwnerName]          varchar(max)       NULL,
    [ContactNumber]      varchar(30)        NULL,
    [CreatedBy]          bigint             NULL,
    [CreatedOn]          datetimeoffset     NULL,
    [ModifiedBy]         bigint             NULL,
    [ModifiedOn]         datetimeoffset     NULL,
    [IsActive]           bit                NULL
    )