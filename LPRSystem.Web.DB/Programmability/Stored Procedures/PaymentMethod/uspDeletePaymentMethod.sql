CREATE PROCEDURE [api].[uspDeletePaymentMethod]
(@id  bigint)

AS

BEGIN

update [data].[PaymentMethod]
       set  IsActive = 0
       WHERE Id = @id

       EXEC  [api].[uspGetPaymentMethods]

   END