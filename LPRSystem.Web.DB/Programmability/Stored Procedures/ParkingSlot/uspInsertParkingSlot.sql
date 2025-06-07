CREATE PROCEDURE [api].[uspInsertParkingSlot]
(
		 @parkingPlaceId bigint null,
		 @parkingSlotCode varchar(50) null,
		 @atmId bigint null,
		 @createdBy bigint null,
		 @createdOn datetimeoffset null,
		 @modifiedBy bigint null,
		 @modifiedOn datetimeoffset null,
		 @isActive bit null
 )

 WITH RECOMPILE

 AS 

 BEGIN

     INSERT INTO [data].[ParkingSlot] (
                                        [ParkingPlaceId],
                                        [ParkingSlotCode],
                                        [ATMId],
                                        [CreatedBy],
                                        [CreatedOn],
                                        [ModifiedBy],
                                        [ModifiedOn],
                                        [IsActive]
                                      )  VALUES
                                      (
                                      @parkingPlaceId,
                                      @parkingSlotCode,
                                      @atmId,
                                      @createdBy,
                                      @createdOn,
                                      @modifiedBy,
                                      @modifiedOn,
                                      @isActive)


END