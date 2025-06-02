CREATE TABLE [dbo].[ParkingPlace]
(
	[ParkingPlaceId]		 BIGINT		NOT NULL	PRIMARY KEY		IDENTITY(1,1),
	[ParkingPlaceName]		 Varchar(Max)   NULL,
	[ParkingPlaceCode]		 Varchar(Max)   NULL,
	[ParkingPlaceType]		 Varchar(Max)   NULL,
	[CreatedOn]				 datetimeoffset	NULL,
	[CreatedBy]				 bigint			NULL,
	[ModifiedOn]			 datetimeoffset	NULL,
	[ModifiedBy]			 bigint			NULL,
	[IsActive]				 bit			NULL
)
