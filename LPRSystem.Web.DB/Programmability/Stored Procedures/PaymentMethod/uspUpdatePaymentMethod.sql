CREATE PROCEDURE [api].[uspUpdatePaymentMethod]
(@id         bigint,
 @Name       varchar(max),
 @Code       varchar(max),
 @CreatedBy  bigint,
 @CreatedOn  datetimeoffset,
 @ModifiedBy bigint,
 @ModifiedOn datetimeoffset,
 @IsActive   bit)
 AS
 BEGIN
 update [data].[paymentMethod] set
   name =      @name,
   code =      @code,
   CreatedBy = @CreatedBy,
   CreatedOn = @CreatedOn,
   ModifiedBy = @ModifiedBy,
   ModifiedOn = @ModifiedOn,
   IsActive  = @IsActive
   Where Id = @id
  
  EXEC [api].[uspGetPaymentMethods]
  END