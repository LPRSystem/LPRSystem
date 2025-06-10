CREATE PROCEDURE [api].[uspInsertOrUpdateParkingTicket]
(
	@ParkingTicket [api].[ParkingTicket] READONLY
)
WITH RECOMPILE

AS

BEGIN

DECLARE @CurrentDate datetimeoffset;
DECLARE @CurrentUser bigint;
Declare @ReturnData [api].[ParkingTicket];

set @CurrentDate = GETDATE(); 

Select @CurrentUser= ModifiedBy from  @ParkingTicket;

Merge INTO [data].[ParkingTicket] AS Target
Using (
   select input.[ParkingTicketId],
		  input.[ParkingTicketCode],
		  input.[ParkingTicketRefrence],
		  input.[ParkedOn],
		  input.[ParkingDurationFrom],
		  input.[ParkingDurationTo],
		  input.[TotalDuration],
		  input.[Price],
		  input.[VehicleNumber],
		  input.[PhoneNumber],
		  input.[CreatedBy],	
		  input.[CreatedOn],
		  input.[ModifiedBy],
		  input.[ModifiedOn],
		  input.[IsActive]
		  FROM @ParkingTicket input
		  LEFT JOIN 
		  [data].[ParkingTicket] par
		  on input.[ParkingTicketId] = par.[ParkingTicketId]
          ) as source
		  ON target.[ParkingTicketId] = source.[ParkingTicketId]
		  WHEN MATCHED THEN
		  UPDATE
		  SET
		  [ParkingTicketId]		= source.[ParkingTicketId],
		  [ParkingTicketCode]	= source.[ParkingTicketCode],
		  [ParkingTicketCode]	= source.[ParkingTicketCode],
		  [ParkedOn]			= source.[ParkedOn],
		  [ParkingDurationFrom]	= source.[ParkingDurationFrom],
		  [ParkingDurationTo]	=source.[ParkingDurationTo],
		  [TotalDuration]		=source.[TotalDuration],
		  [Price]				=source.[Price],
		  [VehicleNumber]		=source.[VehicleNumber],
		  [PhoneNumber]			=source.[PhoneNumber],
		  [CreatedBy]			=source.[CreatedBy],
		  [CreatedOn]			=source.[CreatedOn],
		  [ModifiedBy]			=source.[ModifiedBy],
		  [ModifiedOn]			=source.[ModifiedOn],
		  [IsActive]			=source.[IsActive]
		  WHEN NOT MATCHED BY TARGET THEN 
		  INSERT (
		  [ParkingTicketId],
		  [ParkingTicketCode],
		  [ParkingTicketRefrence],
		  [ParkedOn],
		  [ParkingDurationFrom],
		  [ParkingDurationTo],
		  [TotalDuration],
		  [Price],
		  [VehicleNumber],
		  [PhoneNumber],
		  [CreatedBy],
		  [CreatedOn],
		  [ModifiedBy],
		  [ModifiedOn],
		  [IsActive])
	values
		  (
		  source.[ParkingTicketId],
		  source.[ParkingTicketCode],
		  source.[ParkedOn],
		  source.[ParkingDurationFrom],
		  source.[ParkingDurationTo],
		  source.[TotalDuration],
		  source.[Price],
		  source.[VehicleNumber],
		  source.[PhoneNumber],
		  source.[CreatedBy],
          source.[CreatedOn],
          source.[ModifiedBy],
          source.[ModifiedOn],
          source.[IsActive])
	  OUTPUT 
			 inserted.[ParkingTicketId]
			,inserted.[ParkingTicketCode]
			,inserted.[ParkedOn]
			,inserted.[ParkingDurationFrom]
			,inserted.[ParkingDurationTo]
			,inserted.[TotalDuration]
			,inserted.[Price]
			,inserted.[VehicleNumber]
			,inserted.[PhoneNumber]
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
		  [Price],
		  [VehicleNumber],
		  [PhoneNumber],
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
		  [Price],
		  [VehicleNumber],
		  [PhoneNumber],
		  [CreatedBy],
		  [CreatedOn],
		  [ModifiedBy],
		  [ModifiedOn],
		  [IsActive]
	FROM @ReturnData
END

