Create Type [api].[ParkingPlace] as Table
(
	[ParkingPlaceId]		 BIGINT			NULL,
	[ParkingPlaceName]		 Varchar(Max)   NULL,
	[ParkingPlaceCode]		 Varchar(Max)   NULL,
	[ParkingPlaceType]		 Varchar(Max)   NULL,
	[CreatedOn]				 datetimeoffset	NULL,
	[CreatedBy]				 bigint			NULL,
	[ModifiedOn]			 datetimeoffset	NULL,
	[ModifiedBy]			 bigint			NULL,
	[IsActive]				 bit			NULL
)