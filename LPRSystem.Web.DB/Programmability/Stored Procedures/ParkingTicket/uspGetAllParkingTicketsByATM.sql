CREATE PROCEDURE [api].[uspGetAllParkingTicketsByATM]
    @ATMId     BIGINT,
    @FromDate  DATETIMEOFFSET = NULL,
    @ToDate    DATETIMEOFFSET = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        PT.[ParkingTicketId],
        PT.[ATMId],
        ATM.[ATMCode],
        PT.[ParkingTicketCode],
        PT.[ParkingTicketRefrence],
        PT.[ParkedOn],
        PT.[ParkingDurationFrom],
        PT.[ParkingDurationTo],
        PT.[TotalDuration],
        PT.[ParkingPriceId],
        PT.[VehicleNumber],
        PT.[PhoneNumber],
        PT.[IsExtended],
        PT.[ExtendedOn],
        PT.[ExtendedDurationFrom],
        PT.[ExtendedDurationTo],
        PT.[ActualAmount],
        PT.[ExtendedAmount],
        PT.[TotalAmount],
        PT.[Status],
        PT.[CreatedBy],
        PT.[CreatedOn],
        PT.[ModifiedBy],
        PT.[ModifiedOn],
        PT.[IsActive]
    FROM [data].[ParkingTicket] PT
    INNER JOIN [data].[ATMMachine] ATM ON ATM.ATMId = PT.ATMId
    WHERE
        PT.[ATMId] = @ATMId
        AND PT.[IsActive] = 1
        AND (@FromDate IS NULL OR PT.[ParkedOn] >= @FromDate)
        AND (@ToDate IS NULL OR PT.[ParkedOn] <= @ToDate)
    ORDER BY PT.[ParkedOn] DESC;
END;