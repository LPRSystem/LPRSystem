CREATE PROCEDURE [api].[uspUpdateState]
(
         @stateId bigint,
         @countryId bigint,
         @name varchar(50),
         @description varchar,
         @stateCode varchar(50),
         @modifiedOn datetimeoffset,
         @modifiedBy bigint,
         @isActive bit
)


WITH RECOMPILE

AS

BEGIN

    UPDATE [data].[State]
    SET
        CountryId = @countryId,
        Name = @name,
        Description = @description,
        StateCode = @stateCode,
        ModifiedOn = @modifiedOn,
        ModifiedBy = @modifiedBy,
        IsActive = @isActive
    WHERE StateId = @stateId
END