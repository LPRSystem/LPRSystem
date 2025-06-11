CREATE PROCEDURE [dbo].[uspUpdateCountry]
(
 @countryid  bigint,
 @name       varchar(max),
 @description varchar(max),
 @countrycode varchar(max),
 @modifiedBy bigint,
 @modifiedOn datetimeoffset,
 @isActive   bit)

 AS

 BEGIN

 update [data].[Country] 
		set
		   name =      @name,
		   description = @description,
		   countrycode = @countrycode,
		   ModifiedBy = @modifiedBy,
		   ModifiedOn = @modifiedOn,
		   IsActive  = @isActive
		 Where CountryId = @countryid
  END

