CREATE PROCEDURE [api].[uspGetPaymentMethodById]
(
 @paymentMethodId  bigint
)
WITH RECOMPILE

AS 

BEGIN

SELECT

     [Id]
    ,[Name]
    ,[Code]
    ,[CreatedBy]
    ,[CreatedOn]
    ,[ModifiedBy]
    ,[ModifiedOn]
    ,[IsActive]

    FROM [data].[PaymentMethod]

    WHERE Id=@paymentMethodId

    END