CREATE PROCEDURE [api].[uspDeleteState]
(
    @stateId BIGINT
)

WITH RECOMPILE

AS

BEGIN

    
    DELETE FROM [data].[City] WHERE StateId = @stateId;
    DELETE FROM [data].[State] WHERE StateId = @stateId;

END