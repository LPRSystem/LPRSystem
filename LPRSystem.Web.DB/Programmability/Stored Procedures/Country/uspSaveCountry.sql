CREATE PROCEDURE [api].[uspSaveCountry]
(
		 @name varchar(max),
		 @description varchar(max),
		 @countrycode varchar(max),
		 @createdby bigint,
		 @createdon datetimeoffset,
		 @modifiedby bigint,
		 @modifiedon datetimeoffset,
		 @isactive bit
)
AS

BEGIN
        INSERT INTO [data].[Country]
		(Name,
		Description,
		Countrycode,
		CreatedBy,
		CreatedOn,
		ModifiedBy,
		ModifiedOn,
		IsActive) 
		Values
		(@name,
		@description,
		@countrycode,
		@createdby,
		@createdon,
		@modifiedby,
		@modifiedon,
		@isactive)
END
