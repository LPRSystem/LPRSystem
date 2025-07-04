CREATE PROCEDURE [api].[uspSaveParkingTicket]
    @ParkingTicketId BIGINT OUTPUT,
    @ATMId BIGINT = NULL,
    @ParkingTicketCode NVARCHAR(50) = NULL,
    @ParkingTicketRefrence NVARCHAR(50) = NULL,
    @ParkedOn DATETIMEOFFSET = NULL,
    @ParkingDurationFrom DATETIMEOFFSET = NULL,
    @ParkingDurationTo DATETIMEOFFSET = NULL,
    @TotalDuration BIGINT = NULL,
    @ParkingPriceId BIGINT = NULL,
    @VehicleNumber NVARCHAR(50) = NULL,
    @PhoneNumber NVARCHAR(15) = NULL,
    @IsExtended BIT = NULL,
    @ExtendedOn DATETIMEOFFSET = NULL,
    @ExtendedDurationFrom NVARCHAR(50) = NULL,
    @ExtendedDurationTo NVARCHAR(50) = NULL,
    @ActualAmount DECIMAL(18, 2) = NULL,
    @ExtendedAmount NVARCHAR(50) = NULL,
    @TotalAmount DECIMAL(18, 2) = NULL,
    @Status NVARCHAR(20) = NULL,
    @CreatedBy BIGINT = NULL,
    @CreatedOn DATETIMEOFFSET = NULL,
    @ModifiedBy BIGINT = NULL,
    @ModifiedOn DATETIMEOFFSET = NULL,
    @IsActive BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

 
    INSERT INTO [data].[ParkingTicket] (
        ATMId,
        ParkingTicketCode,
        ParkingTicketRefrence,
        ParkedOn,
        ParkingDurationFrom,
        ParkingDurationTo,
        TotalDuration,
        ParkingPriceId,
        vehicleNumber,
        PhoneNumber,
        IsExtended,
        ExtendedOn,
        ExtendedDurationFrom,
        ExtendedDurationTo,
        ActualAmount,
        ExtendedAmount,
        TotalAmount,
        Status,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES (
        @ATMId,
        @ParkingTicketCode,
        @ParkingTicketRefrence,
        @ParkedOn,
        @ParkingDurationFrom,
        @ParkingDurationTo,
        @TotalDuration,
        @ParkingPriceId,
        @VehicleNumber,
        @PhoneNumber,
        @IsExtended,
        @ExtendedOn,
        @ExtendedDurationFrom,
        @ExtendedDurationTo,
        @ActualAmount,
        @ExtendedAmount,
        @TotalAmount,
        @Status,
        @CreatedBy,
        @CreatedOn,
        @ModifiedBy,
        @ModifiedOn,
        @IsActive
    );

    -- Get the last inserted ParkingTicketId
    SET @ParkingTicketId = SCOPE_IDENTITY();
END