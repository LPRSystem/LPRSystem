CREATE PROCEDURE [api].[uspUpdateOrganization]
(
@id bigint,
@name varchar(50) null,
@code varchar(50) null,
@address varchar(Max) null,
@email varchar(max) null,
@phone varchar(max) null,
@modifiedBy bigint null,
@modifiedOn datetimeoffset null,
@isActive  bit 
)


WITH RECOMPILE

AS

BEGIN

    UPDATE [data].[Organization]
    SET
        Name = @name,
        Code = @code,
        Address = @address,
        Email = @email,
        Phone = @phone,
        ModifiedOn = @modifiedOn,
        ModifiedBy = @modifiedBy,
        IsActive = @isActive
    WHERE Id = @id
END