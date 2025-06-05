CREATE PROCEDURE [api].[uspUpdatePaymentMethod]
(
 @id         bigint,
 @name       varchar(max),
 @code       varchar(max),
 @modifiedBy bigint,
 @modifiedOn datetimeoffset,
 @isActive   bit)

 AS

 BEGIN

 update [data].[paymentMethod] 
		set
		   name =      @name,
		   code =      @code,
		   ModifiedBy = @modifiedBy,
		   ModifiedOn = @modifiedOn,
		   IsActive  = @isActive
		 Where Id = @id
  END

