CREATE PROCEDURE [api].[uspSaveUser]
(
	@firstName varchar(250),
	@lastName varchar(250),
	@email varchar(250),
	@phone varchar(250),
	@passwordHash nvarchar(max),
	@passwordSalt nvarchar(max),
	@roleId bigint,
	@lastPasswordChangedOn datetimeoffset,
	@isBlocked bit,
	@createdBy bigint,
	@modifiedBy bigint,
	@isActive bit
)
AS
BEGIN
INSERT INTO [data].[user]
(
	FirstName,
	LastName,
	Email,
	Phone,
	PasswordHash,
	PasswordSalt,
	RoleId,
	LastPasswordChangedOn,
	IsBlocked,
	CreatedOn,
	CreatedBy,
	ModifiedOn,
	ModifiedBy,
	IsActive
) Values
( 
    @firstName,
	@lastName,
	@email,
	@phone,
	@passwordHash,
	@passwordSalt,
	@roleId,
	@lastPasswordChangedOn,
	@isBlocked,
	GETDATE(),
	@createdBy,
	GETDATE(),
	@modifiedBy,
	@isActive
)
END

