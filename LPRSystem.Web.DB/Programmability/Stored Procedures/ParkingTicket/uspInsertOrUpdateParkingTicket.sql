--CREATE PROCEDURE [api].[uspInsertOrUpdateParkingTicket]
--(
--	@ParkingTicket [api].[ParkingTicket] READONLY
--)
--WITH RECOMPILE

--AS

--BEGIN

--DECLARE @CurrentDate datetimeoffset;
--DECLARE @CurrentUser bigint;
--Declare @ReturnData [api].[ParkingTicket];

Select @CurrentUser= ModifiedBy from @ParkingTicket;

Merge INTO [data].[ParkingTicket] AS Target
Using (
   select   [ParkingTicketId],
		  [ParkingTicketCode],
		  [ParkingTicketRefrence],
		  [ParkedOn],
		  [ParkingDurationFrom],
		  [ParkingDurationTo],
		  [TotalDuration],
		  [ParkingPriceId],
		  [VehicleNumber],
		  [PhoneNumber],
		  [IsExtended],
		  [ExtendedOn],
	      [ExtendedDurationFrom],
	      [ExtendedDurationTo],
	      [ActualAmount],
	      [ExtendedAmount],
	      [TotalAmount],
		  [Status],
		  [CreatedBy],
		  [CreatedOn],
		  [ModifiedBy],
		  [ModifiedOn],
		  [IsActive]
	 FROM @ParkingTicket
	 ) AS source 
	 ON target.[ParkingTicketId] = source.[ParkingTicketId]
	 WHEN MATCHED THEN
	 UPDATE
		  SET
		  [ParkingTicketCode]	= source.[ParkingTicketCode],
		  [ParkingTicketRefrence]= source.[ParkingTicketRefrence],
		  [ParkedOn]			= source.[ParkedOn],
		  [ParkingDurationFrom]	= source.[ParkingDurationFrom],
		  [ParkingDurationTo]	= source.[ParkingDurationTo],
		  [TotalDuration]		= source.[TotalDuration],
		  [ParkingPriceId]		= source.[ParkingPriceId],
		  [VehicleNumber]		= source.[VehicleNumber],
		  [PhoneNumber]			= source.[PhoneNumber],
		  [IsExtended]          = source.[IsExtended],
		  [ExtendedOn]          = source.[ExtendedOn], 
	      [ExtendedDurationFrom]= source.[ExtendedDurationFrom],
	      [ExtendedDurationTo]  = source.[ExtendedDurationTo],
	      [ActualAmount]        = source.[ActualAmount],
	      [ExtendedAmount]      = source.[ExtendedAmount],
	      [TotalAmount]         = source.[TotalAmount],
		  [Status]              = source.[Status],
		  [CreatedBy]			= source.[CreatedBy],
		  [CreatedOn]			= source.[CreatedOn],
		  [ModifiedBy]			= source.[ModifiedBy],
		  [ModifiedOn]			= source.[ModifiedOn],
		  [IsActive]			= source.[IsActive]
		  WHEN NOT MATCHED BY TARGET THEN 
		  INSERT (
		  [ParkingTicketCode],
		  [ParkingTicketRefrence],
		  [ParkedOn],
		  [ParkingDurationFrom],
		  [ParkingDurationTo],
		  [TotalDuration],
		  [ParkingPriceId],
		  [VehicleNumber],
		  [PhoneNumber],
		  [IsExtended],
		  [ExtendedOn],
	      [ExtendedDurationFrom],
	      [ExtendedDurationTo],
	      [ActualAmount],
	      [ExtendedAmount],
	      [TotalAmount],
		  [Status],
		  [CreatedBy],
		  [CreatedOn],
		  [ModifiedBy],
		  [ModifiedOn],
		  [IsActive])
	values
		  (
		  source.[ParkingTicketCode],
		  source.[ParkingTicketRefrence],
		  source.[ParkedOn],
		  source.[ParkingDurationFrom],
		  source.[ParkingDurationTo],
		  source.[TotalDuration],
		  source.[ParkingPriceId],
		  source.[VehicleNumber],
		  source.[PhoneNumber],
		  source.[IsExtended],
		  source.[ExtendedOn],
	      source.[ExtendedDurationFrom],
	      source.[ExtendedDurationTo],
	      source.[ActualAmount],
	      source.[ExtendedAmount],
	      source.[TotalAmount],
		  source.[Status],
		  source.[CreatedBy],
          source.[CreatedOn],
          source.[ModifiedBy],
          source.[ModifiedOn],
          source.[IsActive])
	  OUTPUT 
			 inserted.[ParkingTicketId]
			,inserted.[ParkingTicketCode]
			,inserted.[ParkingTicketRefrence]
			,inserted.[ParkedOn]
			,inserted.[ParkingDurationFrom]
			,inserted.[ParkingDurationTo]
			,inserted.[TotalDuration]
			,inserted.[ParkingPriceId]
			,inserted.[VehicleNumber]
			,inserted.[PhoneNumber]
			,inserted.[IsExtended]
		    ,inserted.[ExtendedOn]
	        ,inserted.[ExtendedDurationFrom]
	        ,inserted.[ExtendedDurationTo]
	        ,inserted.[ActualAmount]
	        ,inserted.[ExtendedAmount]
	        ,inserted.[TotalAmount]
		    ,inserted.[Status]
			,inserted.[CreatedBy]
			,inserted.[CreatedOn]
			,inserted.[ModifiedBy]
			,inserted.[ModifiedOn]
			,inserted.[IsActive]
	INTO 
    @ReturnData(
		  [ParkingTicketId],
		  [ParkingTicketCode],
		  [ParkingTicketRefrence],
		  [ParkedOn],
		  [ParkingDurationFrom],
		  [ParkingDurationTo],
		  [TotalDuration],
		  [ParkingPriceId],
		  [VehicleNumber],
		  [PhoneNumber],
		  [IsExtended],
		  [ExtendedOn],
	      [ExtendedDurationFrom],
	      [ExtendedDurationTo],
	      [ActualAmount],
	      [ExtendedAmount],
	      [TotalAmount],
		  [Status],
		  [CreatedBy],
		  [CreatedOn],
		  [ModifiedBy],
		  [ModifiedOn],
		  [IsActive]);
	SELECT 
		  [ParkingTicketId],
		  [ParkingTicketCode],
		  [ParkingTicketRefrence],
		  [ParkedOn],
		  [ParkingDurationFrom],
		  [ParkingDurationTo],
		  [TotalDuration],
		  [ParkingPriceId],
		  [VehicleNumber],
		  [PhoneNumber],
		  [IsExtended],
		  [ExtendedOn],
	      [ExtendedDurationFrom],
	      [ExtendedDurationTo],
	      [ActualAmount],
	      [ExtendedAmount],
	      [TotalAmount],
		  [Status],
		  [CreatedBy],
		  [CreatedOn],
		  [ModifiedBy],
		  [ModifiedOn],
		  [IsActive]
	FROM @ReturnData
END



