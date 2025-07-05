CREATE PROCEDURE [api].[uspGetAllParkingTicketPayment]

AS

BEGIN

SELECT 

         ParkingTicketPaymentId,
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
from 
        [data].[ParkingTicketPayment]
END