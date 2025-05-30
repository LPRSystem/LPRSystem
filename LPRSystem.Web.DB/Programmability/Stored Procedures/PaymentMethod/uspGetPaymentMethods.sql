CREATE PROCEDURE [api].[uspGetPaymentMethods]
AS
	BEGIN
			SELECT 
			Id,
			Name,
			Code,
			CreatedBy,
			CreatedOn,
			ModifiedBy,
			ModifiedOn,
			IsActive
			from 
			[data].[PaymentMethod]
	END