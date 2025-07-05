CREATE PROCEDURE [api].[uspSaveParkingTicketPayment]
	(
	  @ParkingTicketId    BigInt,
	  @ATMId              BigInt,
	  @PaymentMethodId    BigInt,
	  @PaymentReference   varchar(max),
	  @TotalAmount        Decimal,
	  @PaidAmount         Decimal,
	  @DueAmount          Decimal,
	  @Status             varchar(max),
	  @CreatedBy          BigInt,
	  @CreatedOn          DateTimeOffSet,
	  @ModifiedBy         BigInt,
	  @ModifiedOn         DateTimeOffSet,
	  @IsActive           Bit
	)
	AS

	BEGIN

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
	IsActive)
	Values
	(
	@ParkingTicketId,
	@ATMId,
	@PaymentMethodId,
	@PaymentReference,
	@TotalAmount,
	@DueAmount,
	@Status,
	@CreatedBy,
	@CreatedOn,
	@ModifiedBy,
	@ModifiedOn,
	@IsActive)

END