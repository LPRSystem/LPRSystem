CREATE PROCEDURE [api].[uspGetPaymentDetails]
	(
	@searchString    varchar(50),
	@atmId           bigint
	)
	AS

	BEGIN

	SELECT

	 [ParkingTicketId]
	,[ATMId]
	,[ParkingTicketCode]
	,[ParkingTicketRefrence]
	,[ParkingPriceId]
	,[VehicleNumber]
	,[PhoneNumber]
	,[Status]

	FROM

	   [data].[ParkingTicket]

	END

