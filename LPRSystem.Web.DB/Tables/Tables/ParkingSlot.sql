CREATE TABLE [data].[ParkingSlot]
(
	[ParkingSlotId] [bigint] IDENTITY(1,1) NOT NULL Primary Key,
	[ParkingPlaceId] [bigint] NULL,
	[ParkingSlotCode] [varchar](max) NULL,
	[ATMId] [bigint] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedOn] [datetimeoffset](7) NULL,
	[IsActive] [bit] NULL,
)
