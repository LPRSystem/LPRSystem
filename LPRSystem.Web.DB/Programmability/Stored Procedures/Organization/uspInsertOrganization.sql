CREATE PROCEDURE [api].[uspInsertOrganization]
(

@name varchar(50) null,
@code varchar(50) null,
@address varchar(Max) null,
@email varchar(max) null,
@phone varchar(max) null,
@createdBy  bigint null,
@createdOn datetimeoffset null,
@modifiedBy bigint null,
@modifiedOn datetimeoffset null,
@isActive  bit 
 )

 WITH RECOMPILE

 AS 

 BEGIN

     INSERT INTO [data].[Organization] (
                                       
                                        [Name],
                                        [Code],
                                        [Address],
                                        [Email],
                                        [Phone],
                                        [CreatedBy],
                                        [CreatedOn],
                                        [ModifiedBy],
                                        [ModifiedOn],
                                        [IsActive]
                                      )  VALUES
                                      (
                                    
                                      @name,
                                      @code,
                                      @address,
                                      @email,
                                      @phone,
                                      @createdBy,
                                      @createdOn,
                                      @modifiedBy,
                                      @modifiedOn,
                                      @isActive)


END

