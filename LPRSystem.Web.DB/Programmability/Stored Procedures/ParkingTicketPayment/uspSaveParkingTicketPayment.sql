CREATE PROCEDURE [api].[uspSaveParkingTicketPayment]
(
    @ParkingTicketId     BIGINT,
    @ATMId               BIGINT,
    @PaymentMethodId     BIGINT,
    @PaymentReference    VARCHAR(MAX),
    @TotalAmount         DECIMAL(10, 2),
    @PaidAmount          DECIMAL(10, 2),
    @DueAmount           DECIMAL(10, 2),
    @Status              VARCHAR(MAX),
    @CreatedBy           BIGINT,
    @CreatedOn           DATETIMEOFFSET,
    @ModifiedBy          BIGINT,
    @ModifiedOn          DATETIMEOFFSET,
    @IsActive            BIT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [data].[ParkingTicketPayment]
    (
        ParkingTicketId,
        ATMId,
        PaymentMethodId,
        PaymentReference,
        TotalAmount,
        PaidAmount,
        DueAmount,
        Status,
        CreatedBy,
        CreatedOn,
        ModifiedBy,
        ModifiedOn,
        IsActive
    )
    VALUES
    (
        @ParkingTicketId,
        @ATMId,
        @PaymentMethodId,
        @PaymentReference,
        @TotalAmount,
        @PaidAmount,           
        @DueAmount,
        @Status,
        @CreatedBy,
        @CreatedOn,
        @ModifiedBy,
        @ModifiedOn,
        @IsActive
    )
END
