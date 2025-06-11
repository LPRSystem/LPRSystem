CREATE PROCEDURE [api].[uspInsertState]
(
         @countryId bigint null,
		 @name varchar(50) null,
		 @description varchar null,
         @stateCode varchar(50) null,
         @createdOn datetimeoffset null,
         @createdBy bigint null,
         @modifiedOn datetimeoffset null,
		 @modifiedBy bigint null,
		 @isActive bit null
 )

 WITH RECOMPILE

 AS 

 BEGIN

     INSERT INTO [data].[State] (
                                        [CountryId],
                                        [Name],
                                        [Description],
                                        [StateCode],
                                        [CreatedOn],
                                        [CreatedBy],
                                        [ModifiedOn],
                                        [ModifiedBy],
                                        [IsActive]
                                      )  VALUES
                                      (
                                      @countryId,
                                      @name,
                                      @description,
                                      @stateCode,
                                      @createdOn,
                                      @createdBy,
                                      @modifiedOn,
                                      @modifiedBy,
                                      @isActive)


END